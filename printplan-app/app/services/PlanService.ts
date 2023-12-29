import {RxDatabase} from "rxdb/src/types";
import DatabaseService from "~/services/DatabaseService";
import {Plan} from "~/data/classes";
import {API} from "~/libs/globals";
import {Connectivity} from "@nativescript/core";

export default class PlanService{

  private static _instance: PlanService = new PlanService();

  public static getInstance(): PlanService{
    return PlanService._instance;
  }

  // @ts-ignore
  private database: RxDatabase = undefined;

  private async getDatabase(){
    if (this.database) return;

    console.log("Getting Local database from Service");

    const dbService = DatabaseService.getInstance();

    this.database = await dbService.getDatabase();
  }

  public async getPlans(forceLocal: boolean = false): Promise<Plan[]>{
    if (this.database === undefined) await this.getDatabase();

    const connectionType: number = Connectivity.getConnectionType()

    if (!connectionType || forceLocal){
      console.log("Getting local plans");
      return this.getLocalPlans();
    }

    const webRes = await this.getDistantPlans();

    return webRes;
  }
  private async getDistantPlans(): Promise<Plan[]>{
    const result = await fetch(`${API}/PurePlans`);

    if (!result.ok) return [];

    return await result.json() as Plan[];
  }

  private async getLocalPlans(): Promise<Plan[]>{
    return this.database.plans.find().exec();
  }
}
