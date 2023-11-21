import {FlatList, SafeAreaView} from "react-native";
import SafeViewAndroid from "../../Components/SafeView/SafeViewAndroid";
import SpoolComponent from "../../Components/Spools/SpoolComponent";
import PrinterComponent from "../../Components/Printers/PrinterComponent";

const printers = [
    {
        id: 1,
        name: "Butiful printer",
        printerSpeed: 0.6,
        preheatingDuration: 100
    },
    {
        id: 2,
        name: "Butiful printer",
        printerSpeed: 0.6,
        preheatingDuration: 100
    },
    {
        id: 3,
        name: "Butiful printer",
        printerSpeed: 0.6,
        preheatingDuration: 100
    },
    {
        id: 4,
        name: "Butiful printer",
        printerSpeed: 0.6,
        preheatingDuration: 100
    },
    {
        id: 5,
        name: "Butiful printer",
        printerSpeed: 0.6,
        preheatingDuration: 100
    },
    {
        id: 6,
        name: "Butiful printer",
        printerSpeed: 0.6,
        preheatingDuration: 100
    },
    {
        id: 7,
        name: "Butiful printer",
        printerSpeed: 0.6,
        preheatingDuration: 100
    },
    {
        id: 8,
        name: "Butiful printer",
        printerSpeed: 0.6,
        preheatingDuration: 100
    },

]

export default function PrintersView(){
    return <SafeAreaView style={SafeViewAndroid.AndroidSafeArea}>
        <FlatList
            data={printers}
            renderItem={({item}) => <PrinterComponent printer={item} />}
            keyExtractor={item => item.id}
        />
    </SafeAreaView>
}