<template>
    <with-sidebar>
        <template v-slot:sidebar>
            <q-card>
                <q-card-section>
                    <h5 class="q-mb-lg">Информация</h5>
                    <ul>
                        <li>Предложения, ошибки, вопросы, отзывы в <a href="https://github.com/Naskalin/Relaks/discussions" target="_blank">Github</a></li>
                        <li>Оповещения о выходе новой версии в <a href="https://t.me/+s3jKRAu6YqJiMzUy" target="_blank">Telegram</a>.</li>
                        <li>Программа бесплатна, поэтому не стесняйтесь <a href="https://sobe.ru/na/na_razrabotku_relaks" target="_blank">поддержать проект рублём</a>.</li>
                    </ul>
                </q-card-section>
            </q-card>
        </template>

        <q-card class="q-pa-lg">
            <h1 class="text-secondary"><b>Relaks</b> - тестовый билд</h1>
            <q-card-section>
                <h5 class="q-mb-lg">Relationships calculations and tasks - Взаимоотношения, расчёты и задачи</h5>
                <ul>
                    <li>Это малая часть программы для тестирования.</li>
                    <li>Оффлайн, не требует установки.</li>
                    <li>Можно запускать с внешнего устройства хранения данных, например USB Flash.</li>
                </ul>
                
                <q-separator class="q-my-lg"/>

                <h5 class="q-mb-lg">Объединения</h5>
                <ul>
                    <li>Объединением может быть втреча, компания или человек</li>
                    <li>У объединения может быть название, описание, аватар, рейтинг, а так же типы данных
                        <ul>
                            <li>телефоны (регион и номер)</li>
                            <li>электронные почты</li>
                            <li>ссылки</li>
                            <li>даты</li>
                            <li>текстовые заметки</li>
                            <li>файлы (категории и метки)</li>
                        </ul>
                    </li>
                    <li>Компании и встречи содержат структуры компаний и встреч</li>
                </ul>


                <!--            <q-separator class="q-my-lg"/>-->
                <!--            -->
                <!--            <h5 class="q-mb-lg">Дорожная карта (то, что будет реализовано)</h5>-->
                <!--            <ul>-->
                <!--                <li>Объединение может содержать в себе другие объединения (состав компании, участники встречи)</li>-->
                <!--                <li>Структуры - это произвольные "иерархические блок схемы с перекрёстными связями", которые составляются из объединений, например: кто кем руководит в компании</li>-->
                <!--                <li>Версии для других операционных систем, например Linux и Mac</li>-->
                <!--            </ul>-->

            </q-card-section>
        </q-card>
    </with-sidebar>
</template>

<script setup lang="ts">
import WithSidebar from '../../layouts/_WithSidebar.vue';

</script>