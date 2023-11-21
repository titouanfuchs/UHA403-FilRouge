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
    }
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