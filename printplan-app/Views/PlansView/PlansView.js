import {FlatList, SafeAreaView} from "react-native";
import SafeViewAndroid from "../../Components/SafeView/SafeViewAndroid";
import SpoolComponent from "../../Components/Spools/SpoolComponent";
import PlanComponent from "../../Components/Plans/PlanComponent";

const plans = [
    {
        id: 1,
        printer: 1,
        model: 1,
        spoolReplacementEvents: [],
        initialPrintQuantity: 5,
        printQuantity: 2,
        totalDuration: "00:15:12",
        unitDuration: "00:07:06",
        requiredSpoolQuantity: 1,
        requiredFilamentLenght: 525.0
    }
]

export default function PlansView(){
    return <SafeAreaView style={SafeViewAndroid.AndroidSafeArea}>
        <FlatList
            data={plans}
            renderItem={({item}) => <PlanComponent plan={item} />}
            keyExtractor={item => item.id}
        />
    </SafeAreaView>
}