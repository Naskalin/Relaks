<template>
    <q-table
        class="list-table"
        :title="entryMessages.entryType.pluralNames[store.listRequest.entryType]"
        :columns="columns"
        :loading="store.isLoading"
        row-key="id"
        :rows="store.entries"
        :pagination="{rowsPerPage: 0}"
        :virtual-scroll-item-size="48"
        :virtual-scroll-sticky-size-start="48"
        virtual-scroll
        :rows-per-page-options="[0]"
        @virtual-scroll="getEntries"
    >
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

        <!--                        <template v-slot:top-right="props">-->
        <!--                          <q-btn icon="las la-plus-circle"-->
        <!--                                 @click="addEntryStore.isShowModal = true"-->
        <!--                                 label="Добавить"-->
        <!--                                 color="primary"-->
        <!--                          />-->
        <!--                        </template>-->
    </q-table>
</template>

<script setup lang="ts">
import {useEntryListStore} from "../../../store/entry/entry.list.table.store";
import {entryMessages, actualMessages} from "../../../localize/messages";
import {Entry} from "../../../api/api_types";
import {dateHelper} from "../../../utils/date_helper";

const store = useEntryListStore();

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

// const columns = [
//     {
//         name: 'name',
//         required: true,
//         label: 'Dessert (100g serving)',
//         align: 'left',
//         field: (row: any) => row.name,
//         format: (val: any) => `${val}`,
//         sortable: true
//     },
//     {name: 'calories', align: 'center', label: 'Calories', field: 'calories', sortable: true},
//     {name: 'fat', label: 'Fat (g)', field: 'fat', sortable: true},
//     {name: 'carbs', label: 'Carbs (g)', field: 'carbs'},
//     {name: 'protein', label: 'Protein (g)', field: 'protein'},
//     {name: 'sodium', label: 'Sodium (mg)', field: 'sodium'},
//     {
//         name: 'calcium',
//         label: 'Calcium (%)',
//         field: 'calcium',
//         sortable: true,
//         sort: (a: any, b: any) => parseInt(a, 10) - parseInt(b, 10)
//     },
//     {name: 'iron', label: 'Iron (%)', field: 'iron', sortable: true, sort: (a: any, b: any) => parseInt(a, 10) - parseInt(b, 10)}
// ];

const columns = [
    {name: 'id', label: '#', field: 'id'},
    {name: 'entryType', label: 'Тип', field: (row: Entry) => entryMessages.entryType.names[row.entryType]},
    {name: 'name', label: entryMessages.name[store.listRequest.entryType], field: 'name'},
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
    {name: 'actualStartAtReason', label: actualMessages.actualStartAtReason.name, field: 'actualStartAtReason'},
    {name: 'actualEndAtReason', label: actualMessages.actualEndAtReason.name, field: 'actualEndAtReason'},
]
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
        background-color: $themeBlack;
        color: $bgColor;
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