<template>
    <q-input 
        :label="label || 'Группа'" 
        @click="isShow = !isShow"
        :model-value="selectedStructure ? selectedStructure.title : 'Не выбрано'" 
        readonly>
        <template v-slot:append>
            <q-icon v-if="selectedStructure" name="cancel" @click="structureId = null" class="cursor-pointer"/>
        </template>
    </q-input>
    
    <div class="q-mt-md" v-if="isShow && structureStore.tree.length">
        <div class="q-mb-md q-gutter-x-md">
            <span class="text-h6">Группы</span>
            <q-btn @click="isShow = false" label="Скрыть группы" color="secondary" size="sm" outline icon="las la-eye-slash"/>
        </div>
        <q-tree
            :nodes="structureStore.tree"
            node-key="id"
            v-model:selected="structureId"
            default-expand-all
            no-selection-unset
            selected-color="primary"
            @update:selected="onUpdateSelected"
        >
            <template v-slot:default-header="prop">
                {{ prop.node.data.title }}
            </template>
        </q-tree>
    </div>
</template>

<script setup lang="ts">
import {StructureFormRequest, StructureTree, Structure} from "../../../../api/rerources/api_structure";
import {computed, onMounted, ref} from "vue";
import {useStructureStore} from "../structure_store";

const isShow = ref(true);
const props = defineProps<{
    modelValue: any,
    label?: string
}>()
const structureStore = useStructureStore();
const emit = defineEmits<{
    (e: 'update:modelValue', val: any): void,
}>()
const structureId = computed({
    get: () => props.modelValue,
    set: (val: any) => emit('update:modelValue', val)
});
const selectedStructure = computed(() => {
    if (!structureId.value) return null;
    return structureStore.structuresById[structureId.value];
})

const onUpdateSelected = (val: string | null) => {
    isShow.value = false;
} 
onMounted(async () => {
    // Скрываем дерево, если уже выбрана структура
    if (structureId.value) isShow.value = false;
})
</script>
