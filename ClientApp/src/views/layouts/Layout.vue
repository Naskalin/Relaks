<template>
    <q-layout view="lhh LpR lff"  class="shadow-2 rounded-borders">
        <q-header bordered reveal :reveal-offset="9999" class="bg-secondary text-white fixed-top">
            <q-toolbar>
                <q-tabs>
                    <q-route-tab
                        v-tooltip="layoutStore.isBlockLeavingMessage"
                        :disable="layoutStore.isBlockLeaving"
                        label="Объединения" to="/entries"/>
                </q-tabs>
                <q-space/>

                <q-tabs>
<!--                    <q-route-tab-->
<!--                        v-tooltip="layoutStore.isBlockLeavingMessage"-->
<!--                        :disable="layoutStore.isBlockLeaving"-->
<!--                        label="Шаблоны данных"-->
<!--                        to="/info-templates"/>-->
                    
                    <q-btn-dropdown
                        v-tooltip="layoutStore.isBlockLeavingMessage"
                        :disable="layoutStore.isBlockLeaving"
                        stretch flat label="Общие данные" color="grey-4">
                        <q-list bordered class="bg-secondary" dark>
                            <q-item to="/info-templates" clickable v-close-popup active-class="dark-menu-is-active">
                                <q-item-section>
                                    <q-item-label>Шаблоны [О объединении]</q-item-label>
                                </q-item-section>
                            </q-item>
                        </q-list>
                    </q-btn-dropdown>
                    <q-route-tab
                        to="/"
                        v-tooltip="layoutStore.isBlockLeavingMessage"
                        :disable="layoutStore.isBlockLeaving"
                        alert-icon="las la-info-circle text-positive"
                        label="Relaks"
                        exact
                    >
                        <q-badge color="indigo-8" text-color="white" floating>TEST</q-badge>
                    </q-route-tab>
                </q-tabs>
            </q-toolbar>
        </q-header>

        <q-drawer
            v-model="layoutStore.isLeftSidebar"
            :width="390"
            :breakpoint="700"
            behavior="desktop"
            bordered
            class="bg-my-light"
        >
            <q-scroll-area class="fit">
                <div class="q-py-lg q-px-md" style="margin-top: 51px;">
                    <entry-filter
                        v-if="isListRoute"
                        v-model="entryListStore.listRequest"
                        :preview-entry="entryListStore.previewEntry"
                        @preview-dblclick="onPreviewDbClick"
                    />
                    <router-view name="LeftSidebar"/>
                </div>
            </q-scroll-area>

            <div class="sidebar-header-search bg-secondary fixed-top text-white">
                <q-toolbar class="bg-secondary">
                    <div class="col">
                        <q-input
                            v-model="layoutStore.search"
                            :disable="layoutStore.isBlockLeaving"
                            :autofocus="isListRoute"
                            debounce="250"
                            placeholder="Поиск объединения..."
                            color="secondary"
                            label-color="grey-2"
                            class="text-white"
                            dark
                            borderless
                            dense
                            v-tooltip="layoutStore.isBlockLeavingMessage"
                        >
                            <template v-slot:prepend>
                                <q-icon name="las la-search text-grey-4"/>
                            </template>
                            <template v-slot:append>
                                <q-icon v-if="layoutStore.search" @click="layoutStore.search = ''" name="cancel" class="cursor-pointer text-grey-4"/>
                            </template>
                        </q-input>
                    </div>
                </q-toolbar>
            </div>
        </q-drawer>
        
        <q-drawer
            side="right"
            :model-value="layoutStore.isRightSidebar"
            bordered
            :width="390"
            :breakpoint="700"
            class="bg-my-light"
        >
            <q-scroll-area class="fit" ref="rightScrollAreaRef">
                <div class="q-pt-lg q-pb-xl q-px-md">
                    <router-view name="RightSidebar"/>
                </div>
            </q-scroll-area>
        </q-drawer>

        <q-page-container>
            <q-page padding class="q-pb-xl">
                <router-view/>
            </q-page>

            <q-page-scroller position="bottom">
                <q-btn fab icon="keyboard_arrow_up" color="secondary"/>
            </q-page-scroller>
        </q-page-container>
    </q-layout>
</template>

<script setup lang="ts">
import EntryFilter from '../sections/entry/Entry.List.Filter.vue';
import {ref, onMounted, nextTick, watch, computed} from 'vue';
import {useEntryListStore} from "../../store/entry/entry.list.table.store";
import {useRouter} from "vue-router";
import {useLayoutStore} from "./layout_store";
import {Entry} from "../../api/api_types";
import {emitter} from "../event_bus";
import {QScrollArea} from "quasar";

const rightScrollAreaRef = ref<null | QScrollArea>(null);
const layoutStore = useLayoutStore();
const router = useRouter();
const listFilterEl = ref<any>(null);
const entryListStore = useEntryListStore();
const scrollAreaStyles = ref({
    'margin-top': 20,
});
const isListRoute = computed(() => router.currentRoute.value.name === 'entry-list');
emitter.on('rightSidebarScrollTop', () => {
    if (!rightScrollAreaRef.value) return;
    rightScrollAreaRef.value.setScrollPosition('vertical', 0);
})
watch(() => layoutStore.search, val => {
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
    if (listFilterEl.value) {
        await nextTick();
        const filterHeight = listFilterEl.value.offsetHeight;
        scrollAreaStyles.value['margin-top'] = filterHeight + 20;   
    }
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
.dark-menu-is-active {
    background: lighten($secondary, 5%);
    color: #fff;
}
</style>