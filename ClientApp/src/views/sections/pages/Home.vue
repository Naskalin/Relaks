<template>
    <h1 class="text-secondary"><b>Relaks</b> (TEST build) - программа о взаимоотношениях, расчётах и задачах</h1>
    <div class="row q-col-gutter-lg">
        <div class="col">
            <q-card>
                <q-card-section>
                    <div class="text-h5">О программе</div>
                    <ul class="text-h6">
                        <li>Оффлайн, не требует установки.</li>
                        <li>Можно запускать с внешнего устройства хранения данных, например USB Flash.</li>
                    </ul>
                </q-card-section>
            </q-card>
        </div>
        <div class="col">
            <q-card>
                <q-card-section>
                    <div class="text-h5">Три составляющие Relaks</div>
                    <ol class="text-h6">
                        <li><b>Re</b>lationships - взаимоотношения</li>
                        <li>Calcu<b>la</b>tions - расчёты</li>
                        <li>Tas<b>ks</b> - задачи</li>
                    </ol>
                </q-card-section>
            </q-card>
        </div>
        <div class="col">
            <q-card>
                <q-card-section>
                    <h5 class="q-mb-sm">Есть идея? Мы примем её, если</h5>
                    <ul>
                        <li>Это и/или удобно, полезно, сократит время при работе с программой</li>
                        <li>Это не доставит неудобств другим пользователям</li>
                        <li>Это не идёт в разрез с нашими планами и не ухудшит работу программы</li>
                    </ul>
                    <a href="https://github.com/Naskalin/Relaks/discussions" target="_blank">Предложите свою идею, обсудим :)</a>
                </q-card-section>
            </q-card>
        </div>
    </div>

    <div class="q-my-md"></div>

    <div class="row q-col-gutter-lg">
        <div class="col">
            <q-card>
                <q-card-section>
                    <h5 class="q-mb-sm">Объединения</h5>
                    <ul>
                        <li>Объединением может быть <q-badge class="bg-secondary text-uppercase">встреча</q-badge>, <q-badge class="bg-secondary text-uppercase">компания</q-badge> или <q-badge class="bg-secondary text-uppercase">человек</q-badge></li>
                        <li>У объединения может быть название, описание, аватар, рейтинг, а так же типы данных
                            <ul>
                                <li>телефоны (регион и номер)</li>
                                <li>электронные почты</li>
                                <li>ссылки</li>
                                <li>даты</li>
                                <li>текстовые заметки</li>
                                <li>файлы (со своим поиском, категориями и метками)</li>
                                <li>
                                    <q-badge color="info">НОВОЕ</q-badge>
                                    Структуры, доступные для <q-badge class="bg-secondary text-uppercase">компаний</q-badge> и <q-badge class="bg-secondary text-uppercase">Встреч</q-badge>
                                    <br>
                                    Структуры - это произвольные "иерархические блок схемы с перекрёстными связями", которые составляются из объединений (в том числе людей), например: кто кем руководит в компании или схема судебной встречи со связями
                                </li>
                            </ul>
                        </li>
                    </ul>
                </q-card-section>
            </q-card>
        </div>
        <div class="col">
            <q-card>
                <q-card-section>
                    <h5 class="q-mb-sm">Следующие версии</h5>
                    <p><b>Исправление ошибок, различные доработки</b>, а так же:</p>
                    <ul>
                        <li>Возможность выбора зоны аватара рамкой</li>
                        <li>Возможность прикреплять файлы к наборам данных (о человеке/компании/встрече)</li>
                        <li>Запуск программы в собственном окне</li>
                        <li>Календарь событий с возможностью просмотра будущих/прошлых дат (дни рождения, произвольные даты, даты встреч, регистраций/ликвидаций компаний, даты создания структур/связей и т.д.)</li>
                        <li>Связи по взаимоотношению людей друг к другу с возможностью задать рейтинг связи и её описание</li>
                        <li>
                            Иерархия, доступная для типа объединений <q-badge class="bg-secondary text-uppercase">Человек</q-badge>
                            <br>
                            Иерархия - это генеалогическое древо с перекрёстными связями по взаимоотношению людей друг к другу.
                        </li>
                        <li>Вложенные категории для файлов</li>
                    </ul>
                </q-card-section>
            </q-card>
        </div>
    </div>
</template>

<script setup lang="ts">
// import {useLayoutStore} from "../../layouts/layout_store";
// import {onMounted} from "vue";
//
// const layoutStore = useLayoutStore();
// onMounted(() => {
//     layoutStore.isLeftSidebar = false;
// })
</script>