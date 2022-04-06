<template>
    <div class="row q-col-gutter-md">
        <div class="col-3 q-gutter-y-md">
            <q-card class="q-pa-md">
                <list-filter v-model="store.listRequest"></list-filter>
            </q-card>
            
            <entry-card v-if="previewEntry" :entry="previewEntry" :with-edit="false"></entry-card>
        </div>
        <div class="col">
            <q-table
                class="list-table"
                :title="entryMessages.entryType.pluralNames[store.listRequest.entryType]"
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
                @request="onTableRequest"
                binary-state-sort
            >
                <template v-slot:header-cell-name="props">
                    <q-th :props="props">{{entryMessages.name[store.listRequest.entryType]}}</q-th>
                </template>
                <template v-slot:header-cell-startAt="props">
                    <q-th :props="props">{{entryMessages.startAt[store.listRequest.entryType]}}</q-th>
                </template>
                <template v-slot:header-cell-endAt="props">
                    <q-th :props="props">{{entryMessages.endAt[store.listRequest.entryType]}}</q-th>
                </template>
<!--                <template v-slot:body-cell-avatar="props">-->
<!--                    <q-td :props="props">-->
<!--                        <q-avatar size="50px" font-size="34px" color="grey-5" text-color="grey-4" icon="las la-question-circle" />-->
<!--                    </q-td>-->
<!--                </template>-->
                <template v-slot:body="props">
                    <q-tr :props="props" :key="`m_${props.row.index}`"
                          :class="{
                              'bg-blue-grey-2': previewEntry && previewEntry.id === props.row.id
                          }"
                          @dblclick="rowDoubleClick(props.row)"
                          @click="previewEntry = props.row">
                        <q-td
                            v-for="col in props.cols"
                            :key="col.name"
                            :props="props"
                        >
                            <template v-if="col.name === 'avatar'">
                                <q-avatar size="50px" font-size="34px" color="grey-5" text-color="grey-4" icon="las la-question-circle" />
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
import {watch, ref} from 'vue';
import EntryCard from './Entry.Card.vue';

// initialize
const store = useEntryListStore();
const rowDoubleClick = (row: Entry) => {
    emit('row-dblclick', row);
}
const emit = defineEmits<{
    (e: 'row-dblclick', val: Entry): void,
}>();

const previewEntry = ref<Entry | null>(null);
const columns = [
    // {name: 'id', label: '#', field: 'id'},
    // {name: 'entryType', label: 'Тип', field: (row: Entry) => entryMessages.entryType.names[row.entryType]},
    {name: 'avatar', label: 'Аватар', field: 'id'},
    {name: 'name', label: 'name', field: 'name', sortable: true},
    {name: 'reputation', label: entryMessages.reputation, field: 'reputation', style: 'width: 70px', sortable: true},
    {
        name: 'startAt',
        sortable: true,
        label: entryMessages.startAt[store.listRequest.entryType],
        field: (row: Entry) => row.startAt ? dateHelper.utcFormat(row.startAt) : '',
    },
    {
        name: 'endAt',
        sortable: true,
        label: entryMessages.endAt[store.listRequest.entryType],
        field: (row: Entry) => row.endAt ? dateHelper.utcFormat(row.endAt) : '',
    },
    // {
    //     name: 'actualStartAt',
    //     sortable: true,
    //     label: actualMessages.actualStartAt.name,
    //     field: (row: Entry) => row.actualStartAt ? dateHelper.utcFormat(row.actualStartAt) : '',
    // },
    // {
    //     name: 'actualEndAt',
    //     sortable: true,
    //     label: actualMessages.actualEndAt.name,
    //     field: (row: Entry) => row.actualEndAt ? dateHelper.utcFormat(row.actualEndAt) : '',
    // },
    {
        name: 'updatedAt',
        sortable: true,
        label: entryMessages.updatedAt,
        field: (row: Entry) => dateHelper.utcFormat(row.updatedAt),
    },
    {
        name: 'createdAt',
        sortable: true,
        label: entryMessages.createdAt,
        field: (row: Entry) => dateHelper.utcFormat(row.createdAt),
    },
]

// get entries
const getEntries = async ({to}: { to: number }) => {
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
const onTableRequest = (e: any) => {
    console.log(e);
    // console.log(a);
    // console.log(b);
}
watch([() => store.listRequest.search, () => store.listRequest.entryType], async () => {
    store.listRequest.page = 1;
    store.isEnd = false;
    await getEntries({to: -1})
})
</script>

<style lang="scss">
.list-table {
    max-height: 97vh;

    td, th {
        text-align: left;
    }

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