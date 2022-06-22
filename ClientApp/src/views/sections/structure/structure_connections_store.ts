import {defineStore} from 'pinia';
import {StructureConnection, StructureConnectionDirection} from "../../../api/rerources/api_structure_connections";
import {apiStructureConnection} from "../../../api/rerources/api_structure_connections";
import LeaderLine from "leader-line-new";
import {useStructureStore} from "./structure_store";

// const COLOR = '#ff8a65';
const COLOR = '#ff8a652b';
const ACTIVE_COLOR = '#42a5f5';
const DEFAULT_OPTIONS: LeaderLine.Options = {
    size: 3,
    color: COLOR,
    path: 'fluid'
}

declare type StructureConnectionsStoreState = {
    connections: StructureConnection[],
    connectionLines: { [index: string]: LeaderLine }
}

function updateLineDirection(direction: StructureConnectionDirection, options: LeaderLine.Options): LeaderLine.Options {
    switch (direction) {
        case "Normal":
            options.startPlug = 'disc';
            options.endPlug = 'arrow1';
            // options.startSocket = 'right';
            options.endSocket = 'left';
            break;
        case "Reverse":
            options.startPlug = 'arrow1';
            options.endPlug = 'disc';
            options.startSocket = 'left';
            // options.endSocket = 'right';
            break;
        case "Bidirectional":
            options.startPlug = 'arrow1';
            options.endPlug = 'arrow1';
            options.startSocket = 'left';
            options.endSocket = 'left';
            break;
    }

    return options;
}

export const useStructureConnectionsStore = defineStore('StructureConnectionsStore', {
    state: (): StructureConnectionsStoreState => {
        return {
            connections: [],
            connectionLines: {}
        }
    },
    getters: {
        structureConnections: (state): StructureConnection[] => {
            const structureSelectedId = (useStructureStore()).structureSelectedId;
            if (!state.connections.length || !structureSelectedId) return [];
            return state.connections.filter(
                x => x.structureFirstId === structureSelectedId || x.structureSecondId === structureSelectedId
            )
        }
    },
    actions: {
        async getConnectionsAsync(entryId: string) {
            this.connections = await apiStructureConnection.list({entryId: entryId});
        },
        structureInactiveConnections(structureId: string) {
            if (!this.connections.length) return [];
            return this.connections.forEach(connection => {
                if (connection.structureFirstId === structureId || connection.structureSecondId === structureId) {
                    const line = this.connectionLines[connection.id] || null;
                    if (line) line.color = COLOR;
                }
            })
        },
        drawActiveStructureConnections() {
            if (!this.structureConnections.length) return;
            this.structureConnections.forEach(connection => {
                const line = this.connectionLines[connection.id] || null;
                if (line) line.color = ACTIVE_COLOR;
            })
        },
        drawConnections(connections: StructureConnection[], options = DEFAULT_OPTIONS, connectionIdPrefix = '') {
            const structuresEls = Array.from(document.querySelectorAll('.js-structure-connections'));
            if (!structuresEls.length) return;

            connections.forEach(connection => {
                let connectionItem: { [index: string]: Element } = {};

                [
                    {key: 'first', structureId: connection.structureFirstId},
                    {key: 'second', structureId: connection.structureSecondId}
                ].forEach(item => {
                    const findStructuresEls = structuresEls.filter(el => el.id === item.structureId);
                    if (!findStructuresEls.length) {
                        throw new Error('structureId is missing for StructureConnection');
                    }
                    const boxEl = findStructuresEls[0];
                    const connectionEl = document.createElement('div');
                    connectionEl.id = connectionIdPrefix + item.key + '_' + connection.id;
                    connectionEl.classList.add('structure-connection__el', item.key, connection.direction.toLocaleLowerCase());

                    boxEl.appendChild(connectionEl);
                    connectionItem[item.key] = connectionEl;
                })
                updateLineDirection(connection.direction, options);
                this.connectionLines[connection.id] = new LeaderLine(connectionItem.first, connectionItem.second, options);
            })
        }
    }
})