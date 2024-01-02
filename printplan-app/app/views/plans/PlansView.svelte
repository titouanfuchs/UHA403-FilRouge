<script lang="ts">

    import {onMount} from "svelte";
    import PlanService from "~/services/PlanService";
    import {Plan} from "~/data/classes";
    import {showModal} from "svelte-native";
    import PlanModal from "~/views/plans/PlanModal.svelte";
    import {Dialogs} from "@nativescript/core";

    const planService: PlanService = PlanService.getInstance() as PlanService;

    // @ts-ignore
    let plans: Plan[] = undefined;

    onMount(async ()=>{
        await fetch();
    })

    async function fetch(){
        await remoteSync();
        await getData();
    }

    async function remoteSync(){
        await planService.sync();
    }

    async function getData(){
        // @ts-ignore
        plans = undefined;
        const response = await planService.getPlans();

        plans = response;
    }

    async function showPlan(remoteId: number){
        const plan = await planService.getRemotePlan(remoteId);

        if (!plan){
            Dialogs.alert({
                title: 'Alert!',
                message: 'Impossible d\'afficher les détails de cette planification',
                okButtonText: 'OK',
                cancelable: true,
            })
        }

        await showModal({ page: PlanModal, props: {plan: plan}})
    }

    async function editPlan(plan: Plan){
        Dialogs.prompt({
            title: 'Edition',
            message: 'Quantité a imprimer : ',
            defaultText: plan.quantity.toString(),
            okButtonText: 'Valider',
            neutralButtonText: 'Cancel',
            cancelable: true,
            // cancelButtonText: 'Cancel',
            // capitalizationType: 'none',
            inputType: 'number',
        }).then(async (result) => {
            if (!result.result) return;

            await planService.updatePlan({
                id: plan.remoteId,
                quantity: Number(result.text)
            })

            await getData();
        })
    }

</script>

<page>
    <actionBar title="Planifications">
        <actionItem
                position="left"
                icon="font://&#xf021;"
                class="fas text-xl"
                on:tap="{fetch}"
        />
    </actionBar>
    <stackLayout class="p-5">
        {#if plans === undefined}
            <activityIndicator busy="{true}" />
        {:else if plans.length === 0}
            <label text="Il n'y a pas de planifications"/>
        {:else}
            <scrollView class="min-h-full">
                <stackLayout class="space-y-2">
                    {#each plans as plan}
                        <stackLayout class="border rounded-2xl shadow-2xl bg-white p-5 space-y-2 text-black">
                            <stackLayout>
                                <label class="font-bold text-lg" text="{plan.remoteId}"/>
                                <label text="{plan.printModelId} - En quantité : {plan.quantity}"/>
                            </stackLayout>
                            <flexboxLayout justifyContent="space-between">
                                <button class="fas aspect-square" text="&#xf002;" on:tap={() => {showPlan(plan.remoteId)}}/>
                                <button class="fas aspect-square" text="&#xf044;" on:tap={() => editPlan(plan)}/>
                                <button class="fas bg-red-500 aspect-square" text="&#xf2ed;" on:tap={async () => {await planService.deletePlan(plan.remoteId); await getData()}}/>
                            </flexboxLayout>
                        </stackLayout>
                    {/each}
                </stackLayout>
            </scrollView>
        {/if}
    </stackLayout>
</page>
