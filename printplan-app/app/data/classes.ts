
export interface Plan{
  id?:number|string,
  remoteId:number,
  localId?:string,
  quantity: number,
  printModelId: number
}

export enum OperationType{
  DELETE = "delete",
  ADD = "add",
  EDIT = "edit"
}

export interface LocalOperation{
  id?: string,
  done?: boolean,
  type: OperationType,
  document: string,
  docRemoteId: number,
  values?: string
}

export interface FullPlan {
  spoolReplacementEvents: SpoolReplacementEvent[]
  initialPrintQuantity: number
  printQuantity: number
  totalDuration: string
  unitDuration: string
  requiredSpoolQuantity: number
  requiredFilamentLenght: number
}

export interface SpoolReplacementEvent {
  replacementDate: string
  spoolId: number
}

