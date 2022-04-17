<template>
    <div class="row q-col-gutter-md">
        <div class="col-3 q-gutter-y-md">
            <q-card class="q-pa-md">
                <list-filter v-model="store.listRequest"></list-filter>
            </q-card>
            <entry-card @card-dblclick="emit('card-dblclick', previewEntry)" v-if="previewEntry" :entry="previewEntry" :with-edit="false"></entry-card>
        </div>
        <div class="col">
            <q-table
                ref="entryListTable"
                class="list-table"
                :title="store.listRequest.entryType ? entryMessages.entryType.pluralNames[store.listRequest.entryType] : 'Все'"
                :columns="columns"
                :loading="store.isLoading"
                row-key="id"
                :rows="store.entries"
                :pagination="{rowsPerPage: 0}"
                :virtual-scroll-item-size="65"
                :virtual-scroll-sticky-size-start="65"
                virtual-scroll
                :rows-per-page-options="[0]"
                @virtual-scroll="getEntries"
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
                          @click="previewEntry = props.row">
                        <q-td
                            v-for="col in props.cols"
                            :key="col.name"
                            :props="props"
                        >
                            <q-avatar v-if="col.name === 'avatar'" size="50px" font-size="34px" color="grey-5"
                                      text-color="grey-4"
                                      icon="las la-question-circle"/>
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
        </div>
    </div>
</template>

<script setup lang="ts">
import {useEntryListStore} from "../../../store/entry/entry.list.table.store";
import {entryMessages} from "../../../localize/messages";
import {Entry} from "../../../api/api_types";
import {dateHelper} from "../../../utils/date_helper";
import ListFilter from './Entry.List.Filter.vue';
import {watch, ref, computed} from 'vue';
import EntryCard from './Entry.Card.vue';

// initialize
const store = useEntryListStore();
const rowDoubleClick = (row: Entry) => {
    emit('row-dblclick', row);
}
const emit = defineEmits<{
    (e: 'row-dblclick', val: Entry | any): void,
    (e: 'card-dblclick', val: Entry | any): void
}>();

const previewEntry = ref<Entry | null>(null);
let columns = [
    // {name: 'id', label: '#', field: 'id'},
    {name: 'avatar', label: 'Аватар', field: 'id'},
    {name: 'entryType', label: 'Тип', field: 'entryType', style: 'width: 60px'},
    {name: 'name', label: 'name', field: 'name', sortable: true},
    {name: 'reputation', label: entryMessages.reputation, field: 'reputation', style: 'width: 70px', sortable: true},
    {
        name: 'startAt',
        sortable: true,
        field: 'startAt',
        format: (val: string) => dateHelper.utcFormat(val),
    },
    {
        name: 'endAt',
        sortable: true,
        field: 'endAt',
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

// get entries
const getEntries = async ({to}: { to: number}) => {
    const rowsCount = store.entries.length;
    if (
        // Если ещё нет данных
        to === -1
        // Или если мы в конце списка и данные ещё не закончились
        || (to === rowsCount - 1 && !store.isEnd)
    ) {
        await store.getEntries();
    }
}
watch([
    () => store.listRequest.search,
    () => store.listRequest.entryType,
    () => store.listRequest.isDeleted
], async () => {
    store.listRequest.page = 1;
    store.isEnd = false;
    await getEntries({to: -1})
})

const labelName = computed(() => store.listRequest.entryType ? entryMessages.name[store.listRequest.entryType] : entryMessages.nameNull);
const labelStartAt = computed(() => store.listRequest.entryType ? entryMessages.startAt[store.listRequest.entryType] : entryMessages.startAtNull);
const labelEndAt = computed(() => store.listRequest.entryType ? entryMessages.endAt[store.listRequest.entryType] : entryMessages.endAtNull);
</script>

<style lang="scss">
.list-table {
    max-height: 97vh;

    tfoot tr > *,
    thead tr > * {
        position: sticky;
        opacity: 1;
        z-index: 1;
        background-color: $bgGreyDarken1;
    }

    thead tr:last-child > * {
        top: 0;
    }

    thead tr:first-child > * {
        bottom: 0
    }
}

.q-body--fullscreen-mixin .list-table {
    max-height: 100vh;
}
</style>