<script lang="ts">
    import { closeModal } from 'svelte-native';
    import {CreatePlanDto} from "~/data/classes";
    import {Dialogs} from "@nativescript/core";

    let newPlan: CreatePlanDto = {printerId: 1, printModelId: 1, quantity: 1};

    async function error(){
        await Dialogs.alert({
            title: 'Alert!',
            message: 'Données saisies invalides...',
            okButtonText: 'OK',
            cancelable: true,
        })
    }
</script>

<frame id="detail-page-frame">
<page>
    <stackLayout>
        <label text="Imprimante : {newPlan.printerId}"/>
        <label text="Modèle : {newPlan.printModelId}"/>
        <label text="Quantité : "/>
        <textField keyboardType="integer" bind:text="{newPlan.quantity}" hint="Quantité... (De 1 à + l'infine)" />
        <button text="Valider" on:tap="{ () => newPlan.printerId > 0 && newPlan.printModelId > 0 && newPlan.quantity > 0 ? closeModal(newPlan) :  error()}" />
    </stackLayout>
</page>
</frame>

