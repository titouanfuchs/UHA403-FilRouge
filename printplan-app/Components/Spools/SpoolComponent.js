import {Button, Pressable, StyleSheet, Text, View} from "react-native";

export default function SpoolComponent({spool}){
    return <Pressable style={styles.item}>
        <View style={styles.body}>
            <Text style={styles.name}>{spool.name}</Text>
            <Text>Longueur : {spool.length}</Text>
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