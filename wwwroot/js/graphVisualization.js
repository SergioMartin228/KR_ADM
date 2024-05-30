function renderGraph(nodes, links) {
    const svg = d3.select("#graph-svg");
    svg.selectAll("*").remove();
    const width = +svg.attr("width");
    const height = +svg.attr("height");

    // Определяем маркеры для стрелок
    svg.append("defs").selectAll("marker")
        .data(["end"])
        .enter().append("marker")
        .attr("id", d => d)
        .attr("viewBox", "0 -5 10 10")
        .attr("refX", 34)
        .attr("refY", 0)
        .attr("markerWidth", 4)
        .attr("markerHeight", 4)
        .attr("orient", "auto")
        .append("path")
        .attr("d", "M0,-5L10,0L0,5");

    // Определяем силовую симуляцию
    //const simulation = d3.forceSimulation(nodes)
    //    .force("link", d3.forceLink(links).id(d => d.id).distance(200))
    //    .force("charge", d3.forceManyBody().strength(-400))
    //    .force("center", d3.forceCenter(width / 2, height / 2));
    const simulation = d3.forceSimulation(nodes)
        .force("link", d3.forceLink(links).id(d => d.id).distance(400).strength(0.1))
        .force("charge", d3.forceManyBody().strength(-700)) // Удвоить значение
        .force("center", d3.forceCenter(width / 2, height / 2))
        .force("collide", d3.forceCollide().radius(function (d) { return d.radius * 2; }));

    // Отрисовываем линии для каждой связи
    const link = svg.append("g")
        .attr("class", "links")
        .selectAll("line")
        .data(links)
        .enter().append("line")
        .attr("stroke-width", 3) //d => Math.sqrt(d.capacity)
        .attr("stroke", "black") // Добавьте атрибут stroke, чтобы увидеть рёбра
        .attr("marker-end", "url(#end)");

    // Отрисовываем подписи к связям
    const linkText = svg.append("g")
        .attr("class", "link-labels")
        .selectAll("text")
        .data(links)
        .enter().append("text")
        .attr("dx", 2)
        .attr("dy", "-0.3em") // Слегка сдвинут по высоте
        .style("font", "20px sans-serif") // Увеличенный шрифт для пропускной способности
        .style("fill", "#404040") // Темно-серый цвет текста
        .text(d => `${d.flow}/${d.capacity-d.flow}`);

    // Отрисовываем кружки для каждой вершины
    const node = svg.append("g")
        .attr("class", "nodes")
        .selectAll("circle")
        .data(nodes)
        .enter().append("circle")
        .attr("r", 30)
        .attr("fill", "#98a9d1");

    const labels = svg.append("g")
        .attr("class", "labels")
        .selectAll("text")
        .data(nodes)
        .enter().append("text")
        .attr("dx", -18)
        .attr("dy", ".35em")
        .style("font", "30px sans-serif") // Увеличенный шрифт для подписей вершин
        .text(d => `V${d.id}`);

    const labelOffset = 10;
    // Перерисовка графа на каждый "tick" симуляции
    simulation.on("tick", () => {
        link
            .attr("x1", d => d.source.x)
            .attr("y1", d => d.source.y)
            .attr("x2", d => d.target.x)
            .attr("y2", d => d.target.y);
        //linkText
        //    .attr("x", d => (d.source.x + d.target.x) / 2)
        //    .attr("y", d => (d.source.y + d.target.y) / 2);
        linkText
            .attr("x", d => (d.source.x + d.target.x) / 2)
            .attr("y", d => (d.source.y + d.target.y) / 2)
            .attr("transform", d => {
                const dx = d.target.x - d.source.x;
                const dy = d.target.y - d.source.y;
                const px = (d.source.x + d.target.x) / 2 + dy / length * labelOffset;
                const py = (d.source.y + d.target.y) / 2 - dx / length * labelOffset;
                return `translate(${px}, ${py})`;
            });
        node
            .attr("cx", d => d.x)
            .attr("cy", d => d.y);
        labels // Обновление позиции текстовых меток вершин
            .attr("x", d => d.x)
            .attr("y", d => d.y);
    });
}
