<template>

  <div class="row">
    <div class="col-3">
      <p><q-btn :to="{ name: 'entries-add' }" color="primary">Добавть entry</q-btn></p>
      <p>фильтры...</p>
      <div class="q-pa-md q-gutter-sm">
        <q-btn color="white" text-color="black" label="Standard" />
        <q-btn color="primary" label="Primary" />
        <q-btn color="secondary" label="Secondary" />
        <q-btn color="amber" glossy label="Amber" />
        <q-btn color="brown-5" label="Brown 5" />
        <q-btn color="deep-orange" glossy label="Deep Orange" />
        <q-btn color="purple" label="Purple" />
        <q-btn color="black" label="Black" />
      </div>
    </div>
    <div class="col-9">
      <q-table
          class="list-table"
          :title="'Люди (' + totalResources + ')'"
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
      >

        <template v-slot:top-right="props">
          <q-input borderless dense debounce="300" v-model="filter" placeholder="Поиск...">
            <template v-slot:append>
              <q-icon name="las la-search"/>
            </template>
          </q-input>

          <q-btn
              flat round dense
              :icon="props.inFullscreen ? 'las la-compress-arrows-alt' : 'las la-arrows-alt'"
              @click="props.toggleFullscreen"
              class="q-ml-md"
          ></q-btn>
        </template>
      </q-table>
    </div>
  </div>
</template>

<script lang="ts">
import {defineComponent, ref, Ref, onMounted, computed, reactive} from "vue";
import {date} from 'quasar';
import {jsonApi} from './../../api/index';

const columns = [
  {name: 'id', label: 'Id', field: 'id'},
  {name: 'name', label: 'Имя', field: (row: any) => row.attributes.name},
  {name: 'reputation', label: 'Репутация', field: (row: any) => row.attributes.reputation},
  {
    name: 'birthDay',
    label: 'День рождения',
    field: (row: any) => row.attributes.birthDay,
    format: (val: any) => val ? date.formatDate(new Date(val), 'YYYY-MM-DD HH:mm:ss') : null
  },
]

export default defineComponent({
  name: "ListEntry",
  setup() {
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

        await jsonApi.getAll('/persons', {
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

    return {
      columns,
      rows,
      pagination: {rowsPerPage: 0},
      loadData,
      isLoading,
      totalResources,
      filter
    }
  }
})
</script>

<style lang="scss">
.list-table {
  max-height: 96vh;

  thead tr > * {
    position: sticky;
    opacity: 1;
    z-index: 1;
    background-color: black;
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