import {View, StyleSheet, Text} from "react-native";
import {StatusBar} from "expo-status-bar";

export default function MainView(){
    return <View style={styles.container}>
        <StatusBar style="auto" />
        <Text>Test</Text>
    </View>
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#fff',
        alignItems: 'center',
        justifyContent: 'center',
    },
});
