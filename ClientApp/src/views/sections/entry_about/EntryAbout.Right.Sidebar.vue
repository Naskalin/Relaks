<template>
    <div>
        <div class="text-h6 q-mb-md">Наборы данных</div>
        <q-list v-if="aboutStore.list.length" bordered separator class="bg-my-grey">
            <q-item 
                v-for="eInfo in aboutStore.list" 
                clickable 
                @click="onClickItem(eInfo)"
            >
                <q-item-section avatar>
                    <q-icon name="las la-hand-point-right" />
                </q-item-section>
                <q-item-section class="q-pr-none">
                    <q-item-label :lines="3">{{ getCustomTitle(eInfo) }}</q-item-label>
                </q-item-section>
                <q-item-section side>
                    <q-btn 
                        v-tooltip="eInfo.isFavorite ? 'Убрать из избранного': 'Добавить в избранное'" 
                        icon="star" 
                        :color="eInfo.isFavorite ? 'primary' : 'secondary'" 
                        round 
                        :flat="!eInfo.isFavorite"
                        :outline="eInfo.isFavorite"
                    />
                </q-item-section>
            </q-item>
        </q-list>
        <p class="text-center q-mt-sm" style="font-size: 0.85rem;">
            <i class="q-icon las la-info-circle" aria-hidden="true" role="presentation"> </i> Избранные наборы будут в начале списка.
        </p>
    </div>
</template>

<script setup lang="ts">
import {useEntryAboutStore} from "./entry_about_store";
import {EntryInfo, CustomInfo} from "../../../api/api_types";
import { scroll } from 'quasar'

const aboutStore = useEntryAboutStore();
const getCustomTitle = (eInfo: EntryInfo) => {
    if (eInfo.title) return eInfo.title;
    const custom = eInfo.info as CustomInfo;
    const firstGroup = custom.groups[0];
    if (firstGroup.title) return firstGroup.title;

    const firstItem = firstGroup.items[0];
    return firstItem.value;
}
const onClickItem = (eInfo: EntryInfo) => {
    const { getScrollTarget, setVerticalScrollPosition } = scroll
    const el = document.getElementById('eInfo_custom_'+eInfo.id) as HTMLElement;
    const target = getScrollTarget(el);
    const offset = el.offsetTop
    setVerticalScrollPosition(target, offset, 300);
}
</script>