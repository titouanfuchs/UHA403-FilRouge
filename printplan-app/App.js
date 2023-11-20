import {createMaterialBottomTabNavigator} from "react-native-paper/react-navigation";
import ModelsView from "./Views/Models/ModelsView";
import {NavigationContainer} from "@react-navigation/native";
import {SafeAreaProvider} from "react-native-safe-area-context";
import React from "react";
import MaterialCommunityIcons from "react-native-vector-icons/MaterialCommunityIcons";

const Tab = createMaterialBottomTabNavigator();

export default function App() {
  return (
      <SafeAreaProvider>
        <NavigationContainer>
          <Tab.Navigator initialRouteName={"Models"}>
            <Tab.Screen name={"Models"} component={ModelsView} options={{
              tabBarLabel: 'Models',
              tabBarIcon: ({ color }) => (
                <MaterialCommunityIcons name="video-3d" color={color} size={26} />
              )}}
            />
            <Tab.Screen name="Bobines" component={ModelsView} options={{
                tabBarLabel: 'Bobines',
                tabBarIcon: ({ color }) => (
                    <MaterialCommunityIcons name="lambda" color={color} size={26} />
                )}}/>
            <Tab.Screen name="Imprimantes" component={ModelsView} options={{
                tabBarLabel: 'Imprimantes',
                tabBarIcon: ({ color }) => (
                    <MaterialCommunityIcons name="printer-3d" color={color} size={26} />
                )}}/>
            <Tab.Screen name="Planifications" component={ModelsView} options={{
                tabBarLabel: 'Planifications',
                tabBarIcon: ({ color }) => (
                    <MaterialCommunityIcons name="floor-plan" color={color} size={26} />
                )}}/>
          </Tab.Navigator>
        </NavigationContainer>
      </SafeAreaProvider>
  );
}

