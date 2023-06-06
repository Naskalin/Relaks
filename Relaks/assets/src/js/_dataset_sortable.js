import Sortable from 'sortablejs';

window.SortableJsGroupsInit = async (netDataset) => {
    const groupWrapperEl = document.getElementById('js-sortable-groups');
    if (!groupWrapperEl) throw new Error(`Sortable group element not found`);
    const sortableGroups = Sortable.create(groupWrapperEl, {
        handle: '.js-sortable-handle',
        onSort: async function () {
            await netDataset.invokeMethodAsync('OnSortGroups', sortableGroups.toArray())
        }
    });
};

window.SortableJsItemsInit = async (netDatasetGroup, itemsContainerId) => {
    const itemsContainerEl = document.getElementById(itemsContainerId);
    if (!itemsContainerEl) throw new Error(`Sortable items element not found`);
    const sortableItems = Sortable.create(itemsContainerEl, {
        handle: '.js-sortable-handle',
        onSort: async function () {
            await netDatasetGroup.invokeMethodAsync('OnSortItems', sortableItems.toArray())
        }
    });
};