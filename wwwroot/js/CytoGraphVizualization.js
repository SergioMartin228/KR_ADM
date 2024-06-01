function renderCytoscapeGraph(nodes, edges) {
    console.log("Nodes:", nodes);
    console.log("Edges:", edges);
            var cy = cytoscape({
                container: document.getElementById('cy'),

                boxSelectionEnabled: false,
                autounselectify: true,

                layout: {
                    name: 'cose',
                    idealEdgeLength: 20,
                    nodeOverlap: 20
                },

                style: [
                    {
                        selector: 'node',
                        style: {
                            'width': '30px', // Задайте меньший размер для ширины
                            'height': '30px',
                            'background-color': '#0074D9',
                            'label': 'data(id)',
                            'text-valign': 'center',
                            'text-halign': 'center'
                        }
                    },

                    {
                        selector: 'edge',
                        style: {
                            'width': 2,
                            'line-color': '#000',
                            'mid-target-arrow-color': '#000',
                            'mid-target-arrow-shape': 'triangle',
                            'label': function (ele) {
                                const flow = ele.data('flow');
                                const capacity = ele.data('capacity');
                                const residualCapacity = capacity - flow;
                                return `${flow}/${residualCapacity}`;
                            },
                            'text-margin-y': -6,
                            'text-margin-x': -6,
                            'font-size': '6px',
                        }
                    }
                ],

                elements: {
                    nodes: nodes,
                    edges: edges
                }
            });
    console.log(cy)
        }
