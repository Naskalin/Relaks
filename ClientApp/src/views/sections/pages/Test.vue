<template>
    <with-sidebar>
        <div class="q-pa-md">
            <q-btn color="teal" label="Open Dialog" @click="dialog = true" />

            <q-dialog v-model="dialog">
                <q-layout view="lhh LpR lff" container style="height: 500px" class="bg-grey-3">
                    <q-drawer
                        v-model="drawerLeft"
                        :width="380"
                        behavior="desktop"
                        bordered
                    >
                        <q-scroll-area class="fit">
                            <div class="q-pa-sm">
                                <div v-for="n in 50" :key="n">Drawer {{ n }} / 50</div>
                            </div>
                        </q-scroll-area>
                    </q-drawer>

                    <q-page-container>
                        <q-page class="q-pa-md">
                            <p v-for="n in 15" :key="n">
                                Lorem ipsum dolor sit amet consectetur adipisicing elit. Fugit nihil praesentium molestias a adipisci, dolore vitae odit, quidem consequatur optio voluptates asperiores pariatur eos numquam rerum delectus commodi perferendis voluptate?
                            </p>
                        </q-page>

                        <q-page-scroller position="bottom">
                            <q-btn fab icon="keyboard_arrow_up" color="red" />
                        </q-page-scroller>
                    </q-page-container>
                </q-layout>
            </q-dialog>
        </div>
<!--            <entry-select-field :entry-selected-id="entryId" v-model:is-show="isShow"/>-->
<!--            <q-btn label="show" @click="isShow = !isShow"/>-->
<!--            <p>{{entryId}}</p>-->
<!--            <p>{{isShow}}</p>-->
        
    </with-sidebar>
</template>

<script setup lang="ts">
import {onMounted, computed, ref} from "vue";
import WithSidebar from '../../layouts/_WithSidebar.vue';
import EntrySelectField from '../../fields/EntrySelect.Field.vue';

// const sidebarEl = ref<HTMLElement | null>(null);
// const entryId = ref('');
// const isShow = ref(false);

const dialog = ref(true);
const drawerLeft = ref(true);
</script>
