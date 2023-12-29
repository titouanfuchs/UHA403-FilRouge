<script lang="ts">

    import {onMount} from "svelte";
    import PlanService from "~/services/PlanService";
    import {Plan} from "~/data/classes";

    const planService: PlanService = PlanService.getInstance() as PlanService;

    // @ts-ignore
    let plans: Plan[] = undefined;

    onMount(async ()=>{
        await getData();
    })

    async function getData(){
        // @ts-ignore
        plans = undefined;
        const response = await planService.getPlans();

        plans = response;
    }

</script>

<page>
    <actionBar title="Planifications">
        <actionItem
                position="left"
                icon="font://&#xf021;"
                class="fas text-xl"
                on:tap="{getData}"
        />
    </actionBar>
    <stackLayout class="p-5">
        {#if plans === undefined}
            <activityIndicator busy="{true}" />
        {:else if plans.length === 0}
            <label text="Il n'y a pas de planifications"/>
        {:else}
            <scrollView class="min-h-full border">
                {#each plans as plan}
                    <stackLayout class="border rounded-2xl shadow-2xl bg-white p-5 space-y-2 text-black">
                        <label class="font-bold text-lg" text="PLANIFICATION"/>
                        <label text="Printer - Model"/>
                    </stackLayout>
                {/each}
            </scrollView>
        {/if}
    </stackLayout>
</page>
