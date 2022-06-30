<template>
    <div class="q-gutter-y-sm">
        <deleted :deleted="structure"/>
        <div class="row q-col-gutter-md items-center">
            <div class="col q-gutter-y-sm">
                <div class="text-h6">{{structure.title}}</div>
                <div>
                    <q-icon name="las la-calendar"/>
                    <small class="label-caption q-mx-sm">Существует с</small>
                    <time>{{dateHelper.utcFormat(structure.startAt, 'DD.MM.YYYY')}}</time>
                </div>
                <div v-if="structure.description" class="text-grey-9">
                    <q-icon class="q-mr-xs" name="las la-comment"/>
                    {{structure.description}}
                </div>
                <timestamps :timestamps="structure"/>
            </div>
            <div class="col-auto q-gutter-y-sm">
                <div>
                    <q-btn
                        color="primary"
                        outline
                        icon="las la-edit"
                        round
                        @click="showEditModal"
                        v-tooltip.left="'Изменить группу'"/>
                </div>
                <div>
                    <q-btn
                        color="primary"
                        icon="las la-sitemap"
                        v-tooltip.left="'Добавить подгруппу'"
                        @click="showCreateModalForSelectedStructure"
                        round/>
                </div>
            </div>
        </div>
    </div>
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