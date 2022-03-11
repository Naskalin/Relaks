<template>
  <add-entry></add-entry>
  <div class="row">
    <div class="col-3">
      <q-card class="q-pa-md">
        <list-filter></list-filter>
      </q-card>
    </div>
    <div class="col-9">
      <q-table
          class="list-table q-ml-md"
          :title="'Объединения (' + totalResources + ')'"
          :rows="rows"
          :columns="columns"
          :loading="isLoading"
          row-key="id"
          :pagination="pagination"
          :virtual-scroll-item-size="48"
          :virtual-scroll-sticky-size-start="48"
          virtual-scroll
          :rows-per-page-options="[0]"
          @virtual-scroll="loadData"
          @row-click="onRowClick"
      >
        <template v-slot:body="props">
          <q-tr :props="props" :key="`m_${props.row.index}`" @click.native="onRowClick(props.row.id)">
            <q-td
                v-for="col in props.cols"
                :key="col.name"
                :props="props"
            >
              <template v-if="col.name === 'id'">{{ props.rowIndex + 1 }}</template>
              <template v-else-if="col.name === 'entryType'">{{ entryTypeTrans[col.value] }}</template>
              <template v-else>{{ col.value }}</template>
            </q-td>
          </q-tr>
        </template>

        <template v-slot:top-right="props">
          <q-btn icon="las la-plus-circle"
                 @click="addEntryStore.isShowModal = true"
                 label="Добавить"
                 color="primary"
          />
        </template>
      </q-table>
    </div>
  </div>
</template>

<script lang="ts">
import ListFilter from './_list/ListFilter.vue';
import {defineComponent, ref, Ref, onMounted, computed, reactive} from "vue";
import {useRouter} from 'vue-router'
import {date} from 'quasar';
import {jsonApi} from "../../api";
import AddEntry from "./AddEntry.vue";
import {useEntryAddStore} from "../../store/entry/EntryAddStore";
import {entryTypeTrans} from "../../localize/default";

const columns = [
  {name: 'id', label: '#', field: 'id', style: 'width: 70px'},
  {name: 'entryType', label: 'Тип', field: (row: any) => row.attributes.entryType},
  {name: 'name', label: 'Имя', field: (row: any) => row.attributes.name},
  {name: 'reputation', label: 'Репутация', field: (row: any) => row.attributes.reputation, style: 'width: 70px'},
  {
    name: 'birthDay',
    label: 'День рождения',
    field: (row: any) => row.attributes.birthDay,
    format: (val: any) => val ? date.formatDate(new Date(val), 'YYYY-MM-DD HH:mm:ss') : null
  },
]

export default defineComponent({
  components: {AddEntry, ListFilter},
  setup() {
    const addEntryStore = useEntryAddStore();
    const rows = ref([]);
    const isLoading = ref(false);

    const pageNumber = ref(1);
    const totalResources = ref();
    const filter = ref('');

    const loadData = async ({to}: { to: number }) => {
      const rowsCount = rows.value.length;
      if (to === -1 ||
          (totalResources.value
              && to === rowsCount - 1
              && rowsCount < totalResources.value)
      ) {
        if (isLoading.value) {
          return;
        }
        isLoading.value = true;

        // const apiPerson = ApiPerson;
        //
        // if (filter.value !== '') {
        //   apiPerson.where('name', filter.value);
        // }
        // https://localhost:7125/api/persons?filter[name]=tom&page[number]=1&page[size]=50
        // console.log(ApiPerson.getJsonApiUrl());

        await jsonApi.getAll({resource: 'entries'}, {
          pagination: {
            number: pageNumber.value,
            size: 50
          }
        }).then(resp => {
          rows.value = rows.value.concat(resp.data as any);
          pageNumber.value += 1;
          totalResources.value = resp.meta.total;
        }).finally(() => {
          isLoading.value = false;
        });

        // await ApiPerson
        //     .get(pageNumber.value)
        //     .then(resp => {
        //       rows.value = rows.value.concat(resp.getHttpClientResponse().getData().data);
        //       pageNumber.value += 1;
        //       totalResources.value = resp.getHttpClientResponse().getData().meta.total;
        //       console.log('totalResources', totalResources.value);
        //     }).finally(() => {
        //       isLoading.value = false;
        //     })
      }
    }

    onMounted(async () => {
      // https://localhost:7125/api/persons?filter[name]=eq(tom)&page[number]=1&page[size]=50
      // await ApiPerson.where('name', 'eq(tom)').get();
      // console.log(ApiPerson.where('name', 'eq(tom)').getQuery());
    })

    const router = useRouter();
    return {
      addEntryStore,
      columns,
      rows,
      pagination: {rowsPerPage: 0},
      loadData,
      isLoading,
      totalResources,
      filter,
      entryTypeTrans,
      onRowClick: (id: string) => {
        router.push({name: 'entries-profile', params: {id: id}});
      },
    }
  }
})
</script>

<style lang="scss">
.list-table {
  max-height: 96vh;

  td, th {
    text-align: left;
  }

  tfoot tr > *,
  thead tr > * {
    position: sticky;
    opacity: 1;
    z-index: 1;
    background-color: $grey-9;
    color: white;
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