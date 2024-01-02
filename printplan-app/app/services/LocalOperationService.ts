import {RxDatabase} from "rxdb/src/types";
import DatabaseService from "~/services/DatabaseService";
import {CreatePlanDto, LocalOperation, OperationType} from "~/data/classes";
import {v4} from "@herefishyfish/nativescript-rxdb";
import {API} from "~/libs/globals";

export default class LocalOperationService{
  private static _instance: LocalOperationService = new LocalOperationService();

  public static getInstance(): LocalOperationService{
    return LocalOperationService._instance;
  }

// @ts-ignore
  private database: RxDatabase = undefined;

  private async getDatabase(){
    if (this.database) return;

    console.log("Getting Local database from Service");

    const dbService = DatabaseService.getInstance();

    this.database = await dbService.getDatabase();
  }

  public async isOperationFor(document: string,  documentRemoteId:number): Promise<boolean>{
    if (this.database === undefined) await this.getDatabase();

    let res: LocalOperation[] = await this.database.localOperations.find({selector:{
        document: document,
        docRemoteId: documentRemoteId,
        done: false
      }}).exec();

    return res.length > 0;
  }

  public async getCreateOperations(): Promise<LocalOperation[]>{
    if (this.database === undefined) await this.getDatabase();

    return await this.database.localOperations.find({selector:{type: OperationType.ADD, done: false}}).exec();
  }

  public async setOperationStatus(opId: string){
    if (this.database === undefined) await this.getDatabase();

    await this.database.localOperations.findOne(opId).update({
      $set:{
        done: true
      }
    });
  }

  public async getOperation(id: string): Promise<LocalOperation>{
    if (this.database === undefined) await this.getDatabase();

    return await this.database.localOperations.findOne(id).exec() as LocalOperation;
  }

  public async getOperationsForDocument(document:string, remoteId: number): Promise<LocalOperation[]>{
    if (this.database === undefined) await this.getDatabase();

    return await this.database.localOperations.find({selector:{
        document: document,
        done: false,
        docRemoteId: remoteId
      }}).exec() as LocalOperation[];
  }

  public async addLocalOperation(locOperation: LocalOperation){
    if (this.database === undefined) await this.getDatabase();

    if (locOperation.type === OperationType.DELETE){
      const ops = await this.getOperationsForDocument(locOperation.document, locOperation.docRemoteId!);

      ops.forEach((op) => {
        this.setOperationStatus(op.id!);
      })
    }

    try{
      let op = {...locOperation, id: v4(), done: false};
      await this.database.localOperations.insert(op);

      console.log("Added Operation")
    }catch (e){
      console.log("Cannot save local operation");
    }
  }

  public async execLocalOperation(id:string){
    if (this.database === undefined) await this.getDatabase();

    const op = await this.getOperation(id);
    let success: boolean = false;

    let result;

    switch (op.type){
      case OperationType.ADD:
        console.log("Adding remotely");
        const plan: CreatePlanDto = JSON.parse(op.values!);
        const postMethod = {
          method: 'POST', // Method itself
          headers: {
            'Content-type': 'application/json; charset=UTF-8' // Indicates the content
          },
          body:JSON.stringify(plan)
        }
        result = await fetch(`${API}/Plan`, postMethod);

        success = result.ok;
        break;
      case OperationType.DELETE:
        console.log("Deleting remotely");
        const deleteMethod = {
          method: 'DELETE', // Method itself
          headers: {
            'Content-type': 'application/json; charset=UTF-8' // Indicates the content
          }
        }
        result = await fetch(`${API}/Plan/${op.docRemoteId}`, deleteMethod);

        success = result.ok;
        break;
      case OperationType.EDIT:
        console.log("Edit remotely");

        const patchMethod = {
          method: 'PATCH', // Method itself
          headers: {
            'Content-type': 'application/json; charset=UTF-8' // Indicates the content
          },
          body:JSON.stringify({...JSON.parse(op.values!)
          })
        }
        result = await fetch(`${API}/Plan`, patchMethod);

        success = result.ok;

        break;
    }

    if (!success){
      console.log("Error, local operation was not achieved");
      return;
    }

    await this.setOperationStatus(id);
  }
}
