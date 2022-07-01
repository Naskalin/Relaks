<template>
    <q-drawer
        v-model="drawerLeft"
        :width="390"
        :breakpoint="700"
        behavior="desktop"
        bordered
        class="body--light"
    >
        <q-scroll-area class="fit">
            <div class="q-py-lg q-px-md" style="margin-top: 51px;">
                <entry-filter v-if="isListRoute" v-model="entryListStore.listRequest"/>
                <slot name="sidebar"/>
            </div>
        </q-scroll-area>

        <entry-filter-search v-model="entryListStore.listRequest" :autofocus="isListRoute"/>
    </q-drawer>

    <q-page-container>
        <q-page padding class="q-pb-xl">
            <slot/>
        </q-page>

        <q-page-scroller position="bottom">
            <q-btn fab icon="keyboard_arrow_up" color="secondary"/>
        </q-page-scroller>
    </q-page-container>
</template>

<script setup lang="ts">
import EntryFilter from '../sections/entry/Entry.List.Filter.vue';
import EntryFilterSearch from '../sections/entry/Entry.List.Filter.Search.vue';
import {useQuasar} from 'quasar';
import {ref, onMounted, nextTick, watch, computed} from 'vue';
import {useEntryListStore} from "../../store/entry/entry.list.table.store";
import {useRouter} from "vue-router";

const router = useRouter();
const listFilterEl = ref<any>(null);
const entryListStore = useEntryListStore();
const $q = useQuasar()
const drawerLeft = ref($q.screen.width > 700);
const scrollAreaStyles = ref({
    'margin-top': 20,
});

const isListRoute = computed(() => router.currentRoute.value.name === 'entry-list');
watch(() => entryListStore.listRequest.search, async (val: any) => {
    if (val && val !== '' && !isListRoute.value) {
        await router.push({name: 'entry-list'})
    }
})
watch([
    () => entryListStore.listRequest.search,
    () => entryListStore.listRequest.entryType,
    () => entryListStore.listRequest.isDeleted
], async () => {
    entryListStore.resetListRequest();
    await entryListStore.getEntriesAsync();
})

onMounted(async () => {
    if (!listFilterEl.value) return;
    await nextTick();
    const filterHeight = listFilterEl.value.offsetHeight;
    scrollAreaStyles.value['margin-top'] = filterHeight + 20;
})
</script>