interface Schema{
  version: number,
  primaryKey: string,
  type: string,
  properties: any,
  required: string[]
}

const PlanSchema: Schema = {
  version: 0,
  primaryKey: 'localId',
  type: 'object',
  properties: {
    localId:{
      type: 'string'
    },
    remoteId:{
      type:'number',
      minimum:0,
    },
    quantity: {
      type: 'number',
      minimum: 0
    },
    printModelId:{
      type: 'number',
      minimum:0
    }
  },
  required: ['id', 'printerId', 'quantity', 'printerModelId']
}

const LocalOperationSchema: Schema = {
  version: 0,
  primaryKey: 'id',
  type: 'object',
  properties: {
    id: {
      type:'string',
      maxLength: 100
    },
    done: {
      type:'boolean'
    },          affectedDatabase:{
      type: 'string'
    },
    type:{
      type: 'string'
    },
    document:{
      type: 'string'
    },
    docRemoteId:{
      type: 'number'
    },
    values: {
      type: 'string'
    }
  },
  required: ['id', 'operations', 'done', 'document', 'docRemoteId', 'values']
}

export {PlanSchema, LocalOperationSchema};
