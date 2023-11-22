class Printer extends Realm.Object {
    static schema = {
        name: 'Printer',
        properties: {
            _id: 'objectId',
            name: 'string',
            preheatingDuration: 'Decimal128',
            printerSpeed: 'Decimal128'
        },
        primaryKey: '_id'
    }
}