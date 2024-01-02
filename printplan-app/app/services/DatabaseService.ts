import { addRxPlugin, createRxDatabase, lastOfArray } from 'rxdb';
import { RxDBUpdatePlugin } from 'rxdb/plugins/update';
import { RxDBCleanupPlugin } from 'rxdb/plugins/cleanup';
import {
  getRxStorageLoki
} from 'rxdb/plugins/storage-lokijs';
import { LokiNativescriptAdapter } from '@herefishyfish/nativescript-lokijs-adapter';

import * as DatabaseSchemas from '~/data/schemas';
import {RxDatabase} from "rxdb/src/types";

export default class DatabaseService{

  private static _instance: DatabaseService = new DatabaseService();

  public static getInstance(): DatabaseService{
    return DatabaseService._instance;
  }

  private isInit: boolean = false;
  private database?: RxDatabase;
  async initDatabse(){
    if (this.isInit) return;

    console.log("Initializing Database...");

    addRxPlugin(RxDBUpdatePlugin);
    addRxPlugin(RxDBCleanupPlugin);

    let db = await createRxDatabase({
      name: 'database',
      storage: getRxStorageLoki({
        env: 'NATIVESCRIPT',
        // @ts-ignore
        adapter: new LokiNativescriptAdapter(),
        /*
         * Do not set lokiJS persistence options like autoload and autosave,
         * RxDB will pick proper defaults based on the given adapter
         */
        autosave: true,
        autoload: true
      }),
      multiInstance: false,
      ignoreDuplicate: true,
    });

    await db.addCollections({
      plans: {
        schema: DatabaseSchemas.PlanSchema
      },
      localOperations: {
        schema: DatabaseSchemas.LocalOperationSchema
      }
    });

    // @ts-ignore
    this.database = db;
    this.isInit = true;

    console.log("Database initialized !");
  }

  public async getDatabase(): Promise<RxDatabase>{
    if (!this.isInit) await this.initDatabse();

    return this.database!;
  }

}
