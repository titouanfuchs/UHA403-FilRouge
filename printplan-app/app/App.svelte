<script lang="ts">
    import PrintersView from "~/views/printers/PrintersView.svelte";

    let selectedTab: number = 0;
</script>

<bottomNavigation bind:selectedIndex={selectedTab}>

    <!-- The bottom tab UI is created via TabStrip (the containier) and TabStripItem (for each tab)-->
    <tabStrip>
        <tabStripItem>
            <label text="Planifications" />
            <image src="font://&#xf015;" class="fas t-36" />
        </tabStripItem>
        <tabStripItem>
            <label text="Imprimantes" />
            <image src="font://&#xf007;" class="fas t-36" />
        </tabStripItem>
        <tabStripItem>
            <label text="Bobines" />
            <image src="font://&#xf00e;" class="fas t-36" />
        </tabStripItem>
    </tabStrip>

    <!-- The number of TabContentItem components should corespond to the number of TabStripItem components -->
    <tabContentItem>
        <frame>
        <Reports />
        </frame>
    </tabContentItem>
    <tabContentItem>
        <gridLayout>
            <label text="Account Page" class="h2 text-center" />
        </gridLayout>
    </tabContentItem>
    <tabContentItem>
        <frame>
        <Elements />
        </frame>
    </tabContentItem>
    <tabContentItem>
        <frame>
        <Reporters />
        </frame>
    </tabContentItem>

</bottomNavigation>
