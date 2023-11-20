import {View, Text, StyleSheet, Button,Pressable} from "react-native";

export default function ModelComponent({model}){
    return <Pressable style={styles.item}>
        <View style={styles.body}>
            <Text style={styles.name}>{model.name}</Text>
            <Text>Longueur de filament requis : {model.requiredFilamentLenght}</Text>
        </View>
        <Button style={styles.squareButton} title={"X"} color={"red"}></Button>
    </Pressable>
}

const styles = StyleSheet.create({
    item: {
        borderRadius: 10,
        backgroundColor: '#f5F5F5',
        padding: 20,
        marginVertical: 8,
        marginHorizontal: 16,
        display: "flex",
        flexDirection: "row"
    },
    name:{
        fontWeight: "bold"
    },
    body: {
        flexGrow: 1
    },
    squareButton:{
        aspectRatio: 1
    }
});