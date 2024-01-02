/*
In NativeScript, the app.ts file is the entry point to your application.
You can use this file to perform app-level initialization, but the primary
purpose of the file is to pass control to the app’s first page.
*/

import { svelteNativeNoFrame } from 'svelte-native'
import App from './App.svelte'
import {registerNativeViewElement} from "svelte-native/dom";

registerNativeViewElement("BottomNavigation", () => require("@nativescript-community/ui-material-bottom-navigation").BottomNavigation);
registerNativeViewElement("TabStrip", () => require("@nativescript-community/ui-material-bottom-navigation").TabStrip);
registerNativeViewElement("TabStripItem", () => require("@nativescript-community/ui-material-bottom-navigation").TabStripItem);
registerNativeViewElement("TabContentItem", () => require("@nativescript-community/ui-material-bottom-navigation").TabContentItem);

svelteNativeNoFrame(App, {})
