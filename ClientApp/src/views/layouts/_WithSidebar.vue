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
                <entry-filter 
                    v-if="isListRoute" 
                    v-model="entryListStore.listRequest"
                    :preview-entry="entryListStore.previewEntry"
                    @preview-dblclick="onPreviewDbClick"
                />
                <slot name="sidebar"/>
            </div>
        </q-scroll-area>

        <div class="sidebar-header-search bg-secondary fixed-top text-white">
            <q-toolbar class="bg-secondary">
                <div class="col">
                    <q-input
                        v-model="sidebarStore.search"
                        :autofocus="isListRoute"
                        debounce="250"
                        placeholder="Поиск объединения..."
                        color="secondary"
                        label-color="grey-2"
                        class="text-white"
                        dark
                        borderless
                        dense
                    >
                        <template v-slot:prepend>
                            <q-icon name="las la-search text-grey-2"/>
                        </template>
                        <template v-slot:append>
                            <q-icon v-if="sidebarStore.search" @click="sidebarStore.search = ''" name="cancel" class="cursor-pointer text-grey-2"/>
                        </template>
                    </q-input>
                </div>
            </q-toolbar>
        </div>
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
import {useQuasar} from 'quasar';
import {ref, onMounted, nextTick, watch, computed} from 'vue';
import {useEntryListStore} from "../../store/entry/entry.list.table.store";
import {useRouter} from "vue-router";
import {useWithSidebarStore} from "./with_sidebar_store";
import {Entry} from "../../api/api_types";

const sidebarStore = useWithSidebarStore();
const router = useRouter();
const listFilterEl = ref<any>(null);
const entryListStore = useEntryListStore();
const $q = useQuasar()
const drawerLeft = ref($q.screen.width > 700);
const scrollAreaStyles = ref({
    'margin-top': 20,
});

const isListRoute = computed(() => router.currentRoute.value.name === 'entry-list');
watch(() => sidebarStore.search, val => {
    if (val && val !== '' && !isListRoute.value) {
        router.push({name: 'entry-list'})
    }
    // Передаём значение в entryListStore
    entryListStore.listRequest.search = val;
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
const onPreviewDbClick = (entry: Entry) => {
    router.push({name: 'entry-profile', params: {entryId: entry.id}});
}
</script>

<style lang="scss">
.sidebar-header-search {
    border-bottom: 1px solid $dark;
    z-index: 2000;
}
</style>