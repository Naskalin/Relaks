<template>
    <with-sidebar>
        <template v-slot:sidebar>
            <entry-card v-if="previewEntry" @card-dblclick="emit('card-dblclick', previewEntry)"
                        :entry="previewEntry" :with-edit="false"/>
        </template>

        <q-infinite-scroll ref="scrollEl" @load="onLoadAsync" :disable="store.isEnd" :offset="250">
            <q-table
                ref="entryListTable"
                class="list-table"
                :title="store.listRequest.entryType ? entryMessages.entryType.pluralNames[store.listRequest.entryType] : 'Все'"
                :columns="columns"
                :loading="store.isLoading"
                row-key="id"
                :rows="store.entries"
                :pagination="{rowsPerPage: 0}"
                :rows-per-page-options="[0]"
                binary-state-sort
            >
                <template v-slot:header-cell-name="props">
                    <q-th :props="props">{{ labelName }}</q-th>
                </template>
                <template v-slot:header-cell-startAt="props">
                    <q-th :props="props">{{ labelStartAt }}</q-th>
                </template>
                <template v-slot:header-cell-endAt="props">
                    <q-th :props="props">{{ labelEndAt }}</q-th>
                </template>
                <template v-slot:body="props">
                    <q-tr :props="props" :key="`m_${props.row.index}`"
                          :class="{
                              'bg-pink-1': props.row.deletedAt,
                              'bg-blue-grey-2': previewEntry && previewEntry.id === props.row.id
                          }"
                          @dblclick="rowDoubleClick(props.row)"
                          @click="setPreviewEntry(props.row)">
                        <q-td
                            v-for="col in props.cols"
                            :key="col.name"
                            :props="props"
                        >
                            <q-avatar v-if="col.name === 'avatar'" size="50px" color="grey-5">
                                <api-image v-if="props.row.avatar" :file-id="props.row.avatar" image-filter="square-thumbnail"/>
                                <q-icon v-else name="las la-question-circle" color="grey-4" size="34px"/>
                            </q-avatar>
                            <span v-else-if="col.name === 'entryType'" style="font-size: 1.6rem">
                                <q-icon v-if="col.value === 'Person'" :name="entryMessages.entryType.icons.Person">
                                    <q-tooltip>{{entryMessages.entryType.names.Person}}</q-tooltip>
                                </q-icon>
                                <q-icon v-else-if="col.value === 'Meet'" :name="entryMessages.entryType.icons.Meet">
                                    <q-tooltip>{{entryMessages.entryType.names.Meet}}</q-tooltip>
                                </q-icon>
                                <q-icon v-else :name="entryMessages.entryType.icons.Company">
                                    <q-tooltip>{{entryMessages.entryType.names.Company}}</q-tooltip>
                                </q-icon>
                            </span>
                            <template v-else-if="col.name === 'name'">
                                {{ col.value }}
                                <div v-if="props.row.snippet" class="text-grey-7">
                                    <q-icon name="las la-search" size=".9rem" class="q-mr-xs"/>
                                    <span v-html="props.row.snippet"></span>
                                </div>
                            </template>
                            <template v-else>{{ col.value }}</template>
                        </q-td>
                    </q-tr>
                </template>
                <template v-slot:top-right="props">
                    <slot name="top-right"></slot>
                </template>
            </q-table>
            <template v-slot:loading>
                <div class="row justify-center q-my-md">
                    <q-spinner-dots color="primary" size="40px" />
                </div>
            </template>
        </q-infinite-scroll>
    </with-sidebar>
</template>

<script setup lang="ts">
import WithSidebar from '../../layouts/_WithSidebar.vue';
import ApiImage from '../../components/ApiImage.vue';
import {useEntryListStore} from "../../../store/entry/entry.list.table.store";
import {entryMessages} from "../../../localize/messages";
import {Entry} from "../../../api/api_types";
import {dateHelper} from "../../../utils/date_helper";
import {watch, ref, computed, onMounted} from 'vue';
import EntryCard from './Entry.Card.vue';

const store = useEntryListStore();
const scrollEl = ref<any>(null);
onMounted(async () => {
    if (scrollEl.value) {
        scrollEl.value.trigger();
    }
})
const rowDoubleClick = (row: Entry) => {
    emit('row-dblclick', row);
}
const emit = defineEmits<{
    (e: 'row-click', val: Entry | any): void,
    (e: 'row-dblclick', val: Entry | any): void,
    (e: 'card-dblclick', val: Entry | any): void
}>();

const previewEntry = ref<Entry | null>(null);
let columns = [
    // {name: 'id', label: '#', field: 'id'},
    {name: 'avatar', label: 'Аватар', field: 'id'},
    {name: 'entryType', label: 'Тип', field: 'entryType', style: 'width: 60px'},
    {name: 'name', label: '', field: 'name', sortable: true},
    {name: 'reputation', label: entryMessages.reputation, field: 'reputation', style: 'width: 70px', sortable: true},
    {
        name: 'startAt',
        sortable: true,
        field: 'startAt',
        label: '',
        format: (val: string) => dateHelper.utcFormat(val),
        // sort: (a: string, b: string) => (new Date(a)).getTime() - (new Date(b)).getTime()
    },
    {
        name: 'endAt',
        sortable: true,
        field: 'endAt',
        label: '',
        format: (val: string) => dateHelper.utcFormat(val),
    },
    {
        name: 'updatedAt',
        field: 'updatedAt',
        sortable: true,
        label: entryMessages.updatedAt,
        format: (val: string) => dateHelper.utcFormat(val),
    },
    {
        name: 'createdAt',
        field: 'createdAt',
        sortable: true,
        label: entryMessages.createdAt,
        format: (val: string) => dateHelper.utcFormat(val),
    },
]
columns = columns.map(row => ({...row, align: 'left'}));
const onLoadAsync = async (index: number, done: CallableFunction) => {
    await store.getEntriesAsync();
    done();
}
const setPreviewEntry = (entry: Entry) => {
    previewEntry.value = entry;
    emit('row-click', entry);
}
watch([
    () => store.listRequest.search,
    () => store.listRequest.entryType,
    () => store.listRequest.isDeleted
], async () => {
    store.listRequest.page = 1;
    store.isEnd = false;
    await store.getEntriesAsync();
    
})

const labelName = computed(() => store.listRequest.entryType ? entryMessages.name[store.listRequest.entryType] : entryMessages.nameNull);
const labelStartAt = computed(() => store.listRequest.entryType ? entryMessages.startAt[store.listRequest.entryType] : entryMessages.startAtNull);
const labelEndAt = computed(() => store.listRequest.entryType ? entryMessages.endAt[store.listRequest.entryType] : entryMessages.endAtNull);
</script>

<style lang="scss">
.list-table {
    thead tr > * {
        position: sticky;
        opacity: 1;
        z-index: 1;
        background-color: $bgGreyDarken1;
    }
    thead tr:last-child > * {
        top: 0;
    }
}
</style>