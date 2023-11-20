import {FlatList, SafeAreaView, SectionList, View} from "react-native";
import ModelComponent from "../../Components/Models/ModelComponent";
import {StatusBar} from "expo-status-bar";
import SafeViewAndroid from "../../Components/SafeView/SafeViewAndroid";

const models = [
    {
        id: 1,
        name: "Test Model",
        requiredFilamentLenght: 100.0
    }
]

export default function ModelsView(){
    return <SafeAreaView style={SafeViewAndroid.AndroidSafeArea}>
        <FlatList
            data={models}
            renderItem={({item}) => <ModelComponent model={item} />}
            keyExtractor={item => item.id}
        />
    </SafeAreaView>
}