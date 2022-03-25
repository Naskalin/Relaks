<template>
    <div class="row q-col-gutter-md">
        <div class="col-3 q-gutter-y-md" style="min-width: 320px">
            <q-card class="q-pa-md">
                <list-filter v-model="store.listRequest"></list-filter>
            </q-card>
            
            <entry-card v-if="previewEntry" :entry="previewEntry"></entry-card>
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
                :virtual-scroll-item-size="63"
                :virtual-scroll-sticky-size-start="63"
                virtual-scroll
                :rows-per-page-options="[0]"
                @virtual-scroll="getEntries"
                @row-click="rowClick"
                @row-dblclick="rowDoubleClick"
            >
                <template v-slot:header-cell-name="props">
                    <q-th>{{entryMessages.name[store.listRequest.entryType]}}</q-th>
                </template>
                <template v-slot:header-cell-startAt="props">
                    <q-th>{{entryMessages.startAt[store.listRequest.entryType]}}</q-th>
                </template>
                <template v-slot:header-cell-endAt="props">
                    <q-th>{{entryMessages.endAt[store.listRequest.entryType]}}</q-th>
                </template>
<!--                <template v-slot:body-cell-avatar>-->
<!--                    <q-td>-->
<!--                        <q-avatar>-->
<!--                            <img src="https://via.placeholder.com/100">-->
<!--                        </q-avatar>-->
<!--                    </q-td>-->
<!--                </template>-->
                <template v-slot:body="props">
                    <q-tr :props="props" :key="`m_${props.row.index}`"
                          :class="{
                              'bg-blue-grey-2': previewEntry && previewEntry.id === props.row.id
                          }"
                          @click="previewEntry = props.row">
<!--                          @mouseleave="rowMouseLeave" -->
<!--                          @mouseover="rowMouseOver" -->
                        <q-td
                            v-for="col in props.cols"
                            :key="col.name"
                            :props="props"
                        >
                            <template v-if="col.name === 'avatar'">
                                <q-avatar>
                                    <img src="https://via.placeholder.com/100">
                                </q-avatar>
                            </template>
                            <template v-else>{{ col.value }}</template>
                        </q-td>
                    </q-tr>
                </template>
                <!--                        <template v-slot:body="props">-->
                <!--                          <q-tr :props="props" :key="`m_${props.row.index}`" @click.native="onRowClick(props.row.id)">-->
                <!--                            <q-td-->
                <!--                                v-for="col in props.cols"-->
                <!--                                :key="col.name"-->
                <!--                                :props="props"-->
                <!--                            >-->
                <!--                              <template v-if="col.name === 'id'">{{ props.rowIndex + 1 }}</template>-->
                <!--                              <template v-else-if="col.name === 'entryType'">{{ entryTypeTrans[col.value] || '?' }}</template>-->
                <!--                              <template v-else>{{ col.value }}</template>-->
                <!--                            </q-td>-->
                <!--                          </q-tr>-->
                <!--                        </template>-->

<!--                <template v-slot:top-right="props">-->
<!--                    <q-btn icon="las la-plus-circle"-->
<!--                           @click="addEntryStore.isShowModal = true"-->
<!--                           label="Добавить"-->
<!--                           color="primary"-->
<!--                    />-->
<!--                </template>-->
            </q-table>
        </div>
    </div>
</template>

<script setup lang="ts">
import {useEntryListStore} from "../../../store/entry/entry.list.table.store";
import {entryMessages, actualMessages} from "../../../localize/messages";
import {Entry} from "../../../api/api_types";
import {dateHelper} from "../../../utils/date_helper";
import ListFilter from './Entry.List.Filter.vue';
import {watch, computed, ref} from 'vue';
import EntryCard from './Entry.Profile.Card.vue';

// initialize
const store = useEntryListStore();
const rowDoubleClick = (e: any, row: Entry) => {
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
    {name: 'name', label: 'name', field: 'name'},
    {name: 'reputation', label: entryMessages.reputation, field: 'reputation', style: 'width: 70px'},
    {
        name: 'startAt',
        label: entryMessages.startAt[store.listRequest.entryType],
        field: (row: Entry) => row.startAt ? dateHelper.utcFormat(row.startAt) : '',
    },
    {
        name: 'endAt',
        label: entryMessages.endAt[store.listRequest.entryType],
        field: (row: Entry) => row.endAt ? dateHelper.utcFormat(row.endAt) : '',
    },
    {
        name: 'actualStartAt',
        label: actualMessages.actualStartAt.name,
        field: (row: Entry) => row.actualStartAt ? dateHelper.utcFormat(row.actualStartAt) : '',
    },
    {
        name: 'actualEndAt',
        label: actualMessages.actualEndAt.name,
        field: (row: Entry) => row.actualEndAt ? dateHelper.utcFormat(row.actualEndAt) : '',
    },
    // {name: 'actualStartAtReason', label: actualMessages.actualStartAtReason.name, field: 'actualStartAtReason'},
    // {name: 'actualEndAtReason', label: actualMessages.actualEndAtReason.name, field: 'actualEndAtReason'},
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
watch([() => store.listRequest.search, () => store.listRequest.entryType], async () => {
    store.listRequest.page = 1;
    store.isEnd = false;
    await getEntries({to: -1})
})

// mouse over card

const rowMouseOver = (e: any, row: any) => {
    console.log(e)
}
const rowMouseLeave = (e: any) => {
    console.log(e)
}
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
        //color: $bgColor;
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