import {RxDatabase} from "rxdb/src/types";
import DatabaseService from "~/services/DatabaseService";
import {FullPlan, LocalOperation, OperationType, Plan} from "~/data/classes";
import {API} from "~/libs/globals";
import {Connectivity} from "@nativescript/core";
import {v4} from "@herefishyfish/nativescript-rxdb";
import LocalOperationService from "~/services/LocalOperationService";

export default class PlanService{

  private static _instance: PlanService = new PlanService();

  public static getInstance(): PlanService{
    return PlanService._instance;
  }

  // @ts-ignore
  private database: RxDatabase = undefined;
  private localOperationService?: LocalOperationService = undefined;

  constructor() {

    this.localOperationService = LocalOperationService.getInstance();
  }


  private async getDatabase(){
    if (this.database) return;

    console.log("Getting Local database from Service");

    const dbService = DatabaseService.getInstance();

    this.database = await dbService.getDatabase();
  }

  public async sync(){
    if (this.database === undefined) await this.getDatabase();

    await this.database.plans.cleanup(0);

    const connectionType: number = Connectivity.getConnectionType();

    if (connectionType){
      const webRes = await this.getDistantPlans();
      console.log("Syncing !");

      for (const plan of webRes) {
        const locOperations: LocalOperation[] = await this.localOperationService!.getOperationsForDocument("Plans", plan.id as number);
        if (locOperations.length > 0){

          locOperations.forEach(async (op)=>{
            console.log("Doing local operation " + op.type + " " + op.id);
            await this.localOperationService!.execLocalOperation(op.id!);
          });

          continue;
        }

        const locPlan = await this.getPlan(plan.id! as number);

        if (locPlan === undefined || locPlan === null) {
          console.log(`Adding plan ${plan!.id}`);
          await this.savePlan(plan);
        }
      }

      /*const local: Plan[] = await this.getLocalPlans();

      for (const plan of local){
        if (webRes.findIndex(p => p.id! == plan.id!) !== -1){
          await this.updatePlan(plan);
          continue;
        }

        await this.deletePlan(plan.id!.toString(), true);
      }*/
    }
  }

  public async getPlans(forceLocal: boolean = false): Promise<Plan[]>{
    //if (this.database === undefined) await this.getDatabase();
    let res =  await this.getLocalPlans();

    return res.sort(function(a, b) {return a.remoteId - b.remoteId});
  }

  public async getRemotePlan(remoteId:number): Promise<FullPlan|undefined>{
    const connectionType: number = Connectivity.getConnectionType()

    if (!connectionType) return undefined;

    const result = await fetch(`${API}/Plan/${remoteId}`);

    if (!result.ok) return undefined;

    return await result.json() as FullPlan;
  }

  public async getPlan(remoteId:number): Promise<Plan|undefined|null>{
    if (this.database === undefined) await this.getDatabase();
    let res = undefined;

    try {
      res = await this.database.plans.findOne({selector: {remoteId: remoteId}}).exec();
    }catch (e){
      console.log("Local Plan " + remoteId + " does not exists");
    }

    return res;
  }

  private async getDistantPlans(): Promise<Plan[]>{

    const result = await fetch(`${API}/Plan/PurePlans`);

    if (!result.ok) return [];

    return await result.json() as Plan[];
  }

  private async getLocalPlans(): Promise<Plan[]>{
    return await this.database.plans.find().exec();
  }

  public async savePlan(plan: Plan){
    if (this.database === undefined) await this.getDatabase();
    try{
      await this.database.plans.insert({
        remoteId: plan.id,
        localId: v4(),
        quantity: plan.quantity,
        printModelId: plan.printModelId
      });
    }catch (e){
      console.log("Cannot save plan");
    }
  }

  public async updatePlan(plan: Plan){
    if (this.database === undefined) await this.getDatabase();

    await this.database.plans.findOne(plan.id!.toString()).update({
      $set:{
        quantity: plan.quantity,
        printModelId: plan.printModelId
      }
    });
  }

  public async deletePlan(remoteId: number, localOnly:boolean = false){
    if (this.database === undefined) await this.getDatabase();

    const connectionType: number = Connectivity.getConnectionType()

    if (connectionType && !localOnly){
      const deleteMethod = {
        method: 'DELETE', // Method itself
        headers: {
          'Content-type': 'application/json; charset=UTF-8' // Indicates the content
        }
      }
      const result = await fetch(`${API}/Plan/${remoteId}`, deleteMethod);

      if (!result.ok){
        console.log("Cannot delete remotely -> Storing operation");

        await this.localOperationService!.addLocalOperation({
          document: "Plans",
          docRemoteId: remoteId,
          type: OperationType.DELETE
        });
      }
    }else if (!connectionType){
      console.log("Cannot delete remotely -> Storing operation");

      await this.localOperationService!.addLocalOperation({
        document: "Plans",
        docRemoteId: remoteId,
        type: OperationType.DELETE
      });
    }

    try {
      await this.database.plans.findOne({selector: {remoteId: remoteId}}).remove();
      console.log("Deleted plan localy");
    }catch (e){
      console.log("Local Plan " + remoteId + " does not exists");
    }
  }
}
