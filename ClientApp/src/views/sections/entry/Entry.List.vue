<template>
    <!--      <create-entry @created="entry => $router.push({name: 'entry-profile', params: {entryId: entry.id}})"></create-entry>-->
    <!--  {{ entryCreateStore.model }}-->
<!--    {{ createStore.$state }}-->
<!--    <button @click="isShowCreateForm = true">add</button>-->
<!--    <br>-->
    <entry-form-modal v-model:is-show="isShowCreateForm"
                      v-model="createStore.model"
                      :is-loading="createStore.isLoading"
                      :is-create="true"
                      btn-title="Добавить"
                      @submit="createEntry"
                      title="Добавление объединения"/>
    
    
        
    <entry-list-table @row-dblclick="rowDoubleClick">
        <template v-slot:top-right>
            <q-btn icon="las la-plus-circle"
                   @click="isShowCreateForm = true"
                   label="Добавить"
                   color="primary"
            />
        </template>
    </entry-list-table>
<!--    <div class="row">-->
<!--        <div class="col-3" style="min-width: 320px">-->
<!--            <q-card class="q-pa-md">-->
<!--                <list-filter v-model="listStore.listRequest"></list-filter>-->
<!--            </q-card>-->
<!--        </div>-->
<!--        <div class="col-9">-->
<!--                      :rows="rows"-->
<!--                  <q-table-->
<!--                      class="list-table q-ml-md"-->
<!--                      :title="entryMessages.entryType.pluralNames[listStore.listRequest.entryType]"-->
<!--                      :columns="columns"-->
<!--                      :loading="isLoading"-->
<!--                      row-key="id"-->
<!--                      :pagination="pagination"-->
<!--                      :virtual-scroll-item-size="48"-->
<!--                      :virtual-scroll-sticky-size-start="48"-->
<!--                      virtual-scroll-->
<!--                      :rows-per-page-options="[0]"-->
<!--                      @virtual-scroll="loadData"-->
<!--                  >-->
<!--                    <template v-slot:body="props">-->
<!--                      <q-tr :props="props" :key="`m_${props.row.index}`" @click.native="onRowClick(props.row.id)">-->
<!--                        <q-td-->
<!--                            v-for="col in props.cols"-->
<!--                            :key="col.name"-->
<!--                            :props="props"-->
<!--                        >-->
<!--                          <template v-if="col.name === 'id'">{{ props.rowIndex + 1 }}</template>-->
<!--                          <template v-else-if="col.name === 'entryType'">{{ entryTypeTrans[col.value] || '?' }}</template>-->
<!--                          <template v-else>{{ col.value }}</template>-->
<!--                        </q-td>-->
<!--                      </q-tr>-->
<!--                    </template>-->

<!--                    <template v-slot:top-right="props">-->
<!--                      <q-btn icon="las la-plus-circle"-->
<!--                             @click="addEntryStore.isShowModal = true"-->
<!--                             label="Добавить"-->
<!--                             color="primary"-->
<!--                      />-->
<!--                    </template>-->
<!--                  </q-table>-->
<!--        </div>-->
<!--    </div>-->
</template>

<script setup lang="ts">
// import ListFilter from './Entry.List.Filter.vue';
import EntryFormModal from './Entry.Form.Modal.vue';
import {ref} from 'vue';
import {useEntryCreateStore} from "../../../store/entry/entry.create.store";
// import {useEntryListStore} from "../../../store/entry/entry.list.store";
import {useRouter} from 'vue-router'
import EntryListTable from './Entry.List.Table.vue';
import {Entry} from "../../../api/api_types";

// creating
const router = useRouter();
const createStore = useEntryCreateStore();
const isShowCreateForm = ref(false);
const createEntry = async () => {
    const entry = await createStore.createEntry();
    await router.push({name: 'entry-profile', params: {entryId: entry.id}});
    isShowCreateForm.value = false;
    createStore.$reset();
}

// list
const rowDoubleClick = async (entry: Entry) => {
    await router.push({name: 'entry-profile', params: {entryId: entry.id}});
}
// const listStore = useEntryListStore();

// const columns = [
//   {name: 'id', label: '#', field: 'id', style: 'width: 70px'},
//   {name: 'entryType', label: 'Тип', field: (row: any) => row.attributes.entryType},
//   {name: 'name', label: 'Имя', field: (row: any) => row.attributes.name},
//   {name: 'reputation', label: 'Репутация', field: (row: any) => row.attributes.reputation, style: 'width: 70px'},
//   // {
//   //   name: 'birthDay',
//   //   label: 'День рождения',
//   //   field: (row: any) => row.attributes.birthDay,
//   //   format: (val: any) => val ? date.formatDate(new Date(val), 'YYYY-MM-DD HH:mm:ss') : null
//   // },
// ]

// import CreateEntry from "./Entry.Create.Modal.vue";
// import {useEntryCreateModalStore} from "../../../store/entry/entry.create.modal.store";
//
// const entryCreateStore = useEntryCreateModalStore();

// import {defineComponent, ref, Ref, onMounted, computed, reactive} from "vue";
// import {date} from 'quasar';
// import {useEntryCreateStore} from "../../../store/entry/EntryAddStore";
// import {entryTypeTrans} from "../../../localize/messages";


// export default defineComponent({
//   components: {AddEntry, ListFilter},
//   setup() {
//     const addEntryStore = useEntryCreateStore();
//     const rows = ref([]);
//     const isLoading = ref(false);
//
//     const pageNumber = ref(1);
//     const totalResources = ref();
//     const filter = ref('');
//
//     const loadData = async ({to}: { to: number }) => {
//       const rowsCount = rows.value.length;
//       if (to === -1 ||
//           (totalResources.value
//               && to === rowsCount - 1
//               && rowsCount < totalResources.value)
//       ) {
//         if (isLoading.value) {
//           return;
//         }
//         isLoading.value = true;
//
//         // const apiPerson = ApiPerson;
//         //
//         // if (filter.value !== '') {
//         //   apiPerson.where('name', filter.value);
//         // }
//         // https://localhost:7125/api/persons?filter[name]=tom&page[number]=1&page[size]=50
//         // console.log(ApiPerson.getJsonApiUrl());
//
//         await jsonApi.getAll({resource: 'entries'}, {
//           pagination: {
//             number: pageNumber.value,
//             size: 50
//           }
//         }).then(resp => {
//           rows.value = rows.value.concat(resp.data as any);
//           pageNumber.value += 1;
//           totalResources.value = resp.meta.total;
//         }).finally(() => {
//           isLoading.value = false;
//         });
//
//         // await ApiPerson
//         //     .get(pageNumber.value)
//         //     .then(resp => {
//         //       rows.value = rows.value.concat(resp.getHttpClientResponse().getData().data);
//         //       pageNumber.value += 1;
//         //       totalResources.value = resp.getHttpClientResponse().getData().meta.total;
//         //       console.log('totalResources', totalResources.value);
//         //     }).finally(() => {
//         //       isLoading.value = false;
//         //     })
//       }
//     }
//
//     onMounted(async () => {
//       // https://localhost:7125/api/persons?filter[name]=eq(tom)&page[number]=1&page[size]=50
//       // await ApiPerson.where('name', 'eq(tom)').get();
//       // console.log(ApiPerson.where('name', 'eq(tom)').getQuery());
//     })
//
//     const router = useRouter();
//     return {
//       addEntryStore,
//       columns,
//       rows,
//       pagination: {rowsPerPage: 0},
//       loadData,
//       isLoading,
//       totalResources,
//       filter,
//       entryTypeTrans,
//       onRowClick: (id: string) => {
//         router.push({name: 'entries-profile', params: {id: id}});
//       },
//     }
//   }
// })
</script>