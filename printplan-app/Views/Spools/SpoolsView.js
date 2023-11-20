import {FlatList, SafeAreaView} from "react-native";
import SafeViewAndroid from "../../Components/SafeView/SafeViewAndroid";
import ModelComponent from "../../Components/Models/ModelComponent";
import SpoolComponent from "../../Components/Spools/SpoolComponent";

const spools = [
    {
        id: 1,
        name: "Default Spool",
        length: 1000,
        quantity: 10
    }
]

export default function SpoolView(){
    return <SafeAreaView style={SafeViewAndroid.AndroidSafeArea}>
        <FlatList
            data={spools}
            renderItem={({item}) => <SpoolComponent spool={item} />}
            keyExtractor={item => item.id}
        />
    </SafeAreaView>
}