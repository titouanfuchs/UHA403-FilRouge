<script lang="ts">
    import { closeModal } from 'svelte-native';
    import {FullPlan} from "~/data/classes";

    export let plan: FullPlan;
</script>

<frame id="detail-page-frame">
<page>
    <stackLayout class="space-y-2 p-2">
        <label text="Quantitée d'impressions initiale : {plan.initialPrintQuantity}"/>
        <label text="Quantitée d'impressions finale : {plan.printQuantity}"/>
        <label text="Durée d'impression totale : {plan.totalDuration}"/>
        <label text="Durée d'impression unitaire : {plan.unitDuration}"/>
        <label text="Quantité de fil nécéssaire : {plan.requiredFilamentLenght} m"/>
        <label text="Quantité de bobines de fil nécéssaire : {plan.requiredSpoolQuantity}"/>

        <button text="Fermer" on:tap="{ () => closeModal(undefined) }" />
    </stackLayout>
</page>
</frame>

