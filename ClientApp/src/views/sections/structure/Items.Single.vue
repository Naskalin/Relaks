<template>
    <entry-item 
        v-bind="$attrs"
        :entry="structureItem.entry" 
        clickable
        with-preview
    >
        <template v-slot:body>
            <div v-if="structureItem.description" class="q-mt-xs">
                <q-icon name="las la-comment"/> {{structureItem.description}}
            </div>
            <div class="q-mt-xs text-grey-7">
                <q-icon name="las la-calendar"/>
                <small class="label-caption q-mx-sm">с</small>
                <time>{{dateHelper.utcFormat(structureItem.startAt, 'DD.MM.YYYY')}}</time>
            </div>
        </template>
        <template v-slot:end>
            <q-item-section side v-if="structureItem.deletedAt">
                <q-avatar
                    icon="las la-archive"
                    color="red-8"
                    text-color="white"
                    size="md"
                    v-tooltip="'В архиве с ' + dateHelper.utcFormat(structureItem.deletedAt, 'DD.MM.YYYY')"/>
            </q-item-section>
            <q-item-section side>
                <q-btn color="primary" @click="showEditModal(structureItem)" icon="las la-edit" round flat/>
            </q-item-section>
        </template>
    </entry-item>
</template>

<script setup lang="ts">
import EntryItem from '../../components/entry_item/Entry.Item.vue';
import {StructureItem} from "../../../api/rerources/api_structure_items";
import {dateHelper} from "../../../utils/date_helper";
import {useStructureItemFormStore} from "./structure_items_form_store";
import {useStructureItemsStore} from "./structure_items_store";
import {computed} from 'vue';

const isActive = computed(() => itemsStore.previewItem && props.structureItem.id === itemsStore.previewItem.id)
const props = defineProps<{
    structureItem: StructureItem
}>()
const formStore = useStructureItemFormStore();
const itemsStore = useStructureItemsStore();
const showEditModal = (sItem: StructureItem) => {
    formStore.$reset();
    formStore.editId = sItem.id;
    formStore.request = Object.assign({}, sItem);
    formStore.isShowEdit = true;
}
</script>