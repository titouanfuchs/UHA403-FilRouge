<script lang="ts">
    import PrintersView from "~/views/printers/PrintersView.svelte";
    import PlansView from "~/views/plans/PlansView.svelte";

    let selectedTab: number = 0;
</script>

<bottomNavigation bind:selectedIndex={selectedTab}>
    <!-- The bottom tab UI is created via TabStrip (the containier) and TabStripItem (for each tab)-->
    <tabStrip>
        <tabStripItem>
            <label text="Planifications" />
            <image src="font://&#xf073;" class="fas text-2xl" />
        </tabStripItem>
    </tabStrip>

    <!-- The number of TabContentItem components should corespond to the number of TabStripItem components -->
    <tabContentItem>
        <frame>
            <PlansView/>
        </frame>
    </tabContentItem>

</bottomNavigation>
