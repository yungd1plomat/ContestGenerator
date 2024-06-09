async function updateEvaluations() {
    const response = await fetch(window.location + "/evaluations");
    let json = await response.json();
    console.log(json);
    allHtml = '';
    json.forEach(evaluation => {
        console.log(evaluation)
        let row = `<div class="flex flex-wrap px-3 -mx-3 mb-2 text-left break-normal">
                    <label class="block text-gray-700 text-lg font-bold mr-2">
                            ${evaluation['username']}: 
                    </label>
                   <label class="block text-gray-700 text-lg font-normal">
                            ${evaluation['evaluation']}
                   </label>
               </div>`;
        allHtml += row;
    });
    $('.evaluations').html(allHtml);
}
$(document).ready(async function () {
    setInterval(updateEvaluations, 500);
});