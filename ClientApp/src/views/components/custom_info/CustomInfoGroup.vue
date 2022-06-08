<template>
    <div class="row q-col-gutter-md">
        <div class="col-auto">
            <q-btn
                icon="las la-random"
                color="secondary"
                class="draggable-group-btn"
                outline
                round
                v-tooltip.left="'Переместить группу'"/>
        </div>
        <div class="col-auto">
            <q-btn @click="isMinimize = !isMinimize"
                   :icon="isMinimize ? 'las la-eye-slash' : 'las la-eye'"
                   outline
                   round
                   v-tooltip.left="isMinimize ? 'Раскрыть' : 'Минимизировать'"
                   color="positive"/>
        </div>
        <div class="col">
            <q-input v-model="groupModel.title" filled label="Название группы" counter maxlength="250"/>  
        </div>
        <div class="col-auto">
            <q-btn
                v-if="!isMinimize"
                @click="emit('delete')"
                color="negative" 
                icon="las la-trash"
                v-tooltip="'Удалить группу'"
                outline
                round
                />
        </div>
    </div>
    <q-markup-table v-if="!isMinimize && groupModel.items.length" flat>
        <draggable
            v-model="groupModel.items"
            tag="tbody"
            handle=".draggable-btn"
            :itemKey="new String()"
        >
            <template #item="{element, index}">
                <tr class="q-tr--no-hover">
                    <td v-if="groupModel.items.length > 1" style="width: 50px" class="vertical-top">
                        <q-btn icon="las la-random" color="secondary" class="draggable-btn" outline round size="sm" v-tooltip.left="'Переместить строку'"/>
                    </td>
                    <td style="width: 400px">
                        <q-input v-model="groupModel.items[index].key" filled counter dense maxlength="250" autogrow>
                            <template v-slot:prepend>
                                <q-icon name="las la-key" />
                            </template>
                        </q-input>
                    </td>
                    <td>
                        <q-input v-model="groupModel.items[index].value" filled counter dense maxlength="250" autogrow>
                            <template v-slot:prepend>
                                <q-icon name="las la-comment" />
                            </template>
                        </q-input>
                    </td>
                    <td style="width: 50px">
                        <q-btn
                            v-if="groupModel.items.length > 1"
                            @click="removeItem(index)" 
                            v-tooltip="'Удалить строку'" 
                            icon="las la-times" 
                            color="negative" 
                            outline 
                            size="sm" 
                            round />
                    </td>
                </tr>
            </template>
        </draggable>
    </q-markup-table>
    <div v-if="!isMinimize" class="q-mt-md">
        <q-btn label="Добавить строку" icon="las la-plus-circle" color="secondary" outline @click="addItem"/>
    </div>
</template>

<script setup lang="ts">
import draggable from 'vuedraggable';
import {CustomInfoGroup} from "../../../api/api_types";
import {computed, ref, watch} from "vue";

const props = defineProps<{
    modelValue: CustomInfoGroup,
    isMinimize: boolean
}>();
const emit = defineEmits<{
    (e: 'update:modelValue', val: CustomInfoGroup): void,
    (e: 'delete'): void,
}>();
const groupModel = computed({
    get: () => props.modelValue,
    set: (val: CustomInfoGroup) => emit('update:modelValue', val),
});
const isMinimize = ref(false);
watch(() => props.isMinimize, (val: boolean) => {
    isMinimize.value = val;
})
const addItem = () => {
    groupModel.value.items.push({key: '', value: ''});
}
const removeItem = (index: number) => {
    groupModel.value.items.splice(index, 1);
}
</script>