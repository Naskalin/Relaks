<template>
    <q-card>
        <q-card-section>
            <deleted :deleted="structure"/>
            <div class="q-gutter-y-sm">
                <div style="font-size: 1.1rem">{{structure.title}}</div>
                <div>
                    <q-icon name="las la-calendar"/>
                    <small class="label-caption q-mx-sm">с</small>
                    <time>{{dateHelper.utcFormat(structure.startAt, 'DD.MM.YYYY')}}</time>
                </div>
                <div v-if="structure.description" class="text-grey-9">
                    <q-icon class="q-mr-xs" name="las la-comment"/>
                    {{structure.description}}
                </div>
                <timestamps :timestamps="structure"/>
            </div>
        </q-card-section>
        <q-card-actions align="between">
            <q-btn
                color="primary"
                outline
                icon="las la-edit"
                label="Изменить"
                @click="showEditModal"
                v-tooltip.bottom="'Изменить группу'"
            />
            <q-btn
                color="primary"
                icon="las la-sitemap"
                label="Подгруппа"
                v-tooltip.bottom="'Добавить подгруппу'"
                @click="showCreateModalForSelectedStructure"
            />
        </q-card-actions>
    </q-card>
</template>

<script setup lang="ts">
import {useStructureFormStore} from "./structure_form_store";
import {dateHelper} from "../../../utils/date_helper";
import Deleted from '../../components/Deleted.vue';
import {Structure} from "../../../api/rerources/api_structure";
import Timestamps from '../../components/Timestamps.vue';

const structureFormStore = useStructureFormStore();
const props = defineProps<{
    structure: Structure
}>()
const showCreateModalForSelectedStructure = () => {
    structureFormStore.$reset();
    structureFormStore.request.parentId = props.structure.id;
    structureFormStore.isShowCreate = true
}
const showEditModal = () => {
    structureFormStore.$reset();
    structureFormStore.editId = props.structure.id;
    structureFormStore.request = Object.assign({}, props.structure);
    structureFormStore.isShowEdit = true
}
</script>