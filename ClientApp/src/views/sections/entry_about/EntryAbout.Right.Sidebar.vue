<template>
    <div>
        <div class="text-h6 q-mb-md">Наборы данных</div>
        <q-list v-if="aboutStore.list.length" bordered separator class="bg-my-grey">
            <q-item
                v-for="eInfo in aboutStore.list"
                clickable
                @click="onClickItem(eInfo)"
            >
                <q-item-section class="q-pr-none">
                    <q-item-label :lines="3">{{ getCustomTitle(eInfo) }}</q-item-label>
                </q-item-section>
                <q-item-section side>
                    <q-btn
                        v-tooltip="eInfo.isFavorite ? 'Убрать из избранного': 'Добавить в избранное'"
                        icon="star"
                        size="sm"
                        :color="eInfo.isFavorite ? 'secondary' : 'bg-blue-grey-3'"
                        round
                        :flat="!eInfo.isFavorite"
                        :outline="eInfo.isFavorite"
                        @click="isFavoriteSwitchAsync(eInfo)"
                    />
                </q-item-section>
            </q-item>
        </q-list>
        <p class="text-center q-mt-sm" style="font-size: 0.85rem;">
            <i class="q-icon las la-info-circle" aria-hidden="true" role="presentation"> </i> Избранные наборы будут
            первыми.
        </p>
    </div>
</template>

<script setup lang="ts">
import {useEntryAboutStore} from "./entry_about_store";
import {EntryInfo, CustomInfo} from "../../../api/api_types";
import {scroll} from 'quasar'
import {apiEntryInfo} from "../../../api/rerources/api_entry_info";
import {useRoute} from "vue-router";

const aboutStore = useEntryAboutStore();
const route = useRoute();
const entryId = (useRoute()).params.entryId as string;
const getCustomTitle = (eInfo: EntryInfo) => {
    if (eInfo.title) return eInfo.title;
    const custom = eInfo.info as CustomInfo;
    const firstGroup = custom.groups[0];
    if (firstGroup.title) return firstGroup.title;

    const firstItem = firstGroup.items[0];
    return firstItem.value === '' ? '???' : firstItem.value;
}
const onClickItem = (eInfo: EntryInfo) => {
    // Если мы в режиме изменения/добавления, то не скролим, иначе ошибки сыпятся поскольку элементы для скрола скрыты
    if (route.name !== 'entry-about-list') return;

    const {getScrollTarget, setVerticalScrollPosition} = scroll
    const el = document.getElementById('eInfo_custom_' + eInfo.id) as HTMLElement;
    const target = getScrollTarget(el);
    const offset = el.offsetTop
    setVerticalScrollPosition(target, offset, 200);
}
const isFavoriteSwitchAsync = async (eInfo: EntryInfo) => {
    eInfo.isFavorite = !eInfo.isFavorite;
    await apiEntryInfo.update(entryId, eInfo.id, eInfo);
    await aboutStore.getItemsAsync(entryId);
    onClickItem(eInfo);
}
</script>