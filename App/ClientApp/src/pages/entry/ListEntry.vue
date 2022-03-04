<template>
  <div>
    <p>
      <q-btn :to="{ name: 'entries-add' }" color="primary">Добавть entry</q-btn>
    </p>

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
        <q-input borderless dense debounce="300" v-model="filter" placeholder="Search">
          <template v-slot:append>
            <q-icon name="search" />
          </template>
        </q-input>
        
        <q-btn
            flat round dense
            :icon="props.inFullscreen ? 'fullscreen_exit' : 'fullscreen'"
            @click="props.toggleFullscreen"
            class="q-ml-md"
        ></q-btn>
      </template>
    </q-table>
  </div>
</template>

<script lang="ts">
import {defineComponent, ref, Ref, onMounted, computed, reactive} from "vue";
import {ApiPerson} from "../../types/api";
import {date} from 'quasar';

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
        
        const q = ApiPerson;
        
        if (filter.value !== '') {
          q.where('name', filter.value);
        }

        await q
            .get(pageNumber.value)
            .then(resp => {
              rows.value = rows.value.concat(resp.getHttpClientResponse().getData().data);
              pageNumber.value += 1;
              totalResources.value = resp.getHttpClientResponse().getData().meta.totalResources;
            }).finally(() => {
              isLoading.value = false;
            })
      }
    }

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
  max-height: 80vh;

  thead tr > *,
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