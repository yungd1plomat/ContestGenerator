var reviewIndex = 0;
var photoIndex = 0;
var stepIndex = 0;
var fieldIndex = 0;
var helpIndex = 0;
var partnerIndex = 0;
var nominationIndex = 0;
var newsIndex = 0;
function addRules() {
    const existingTextarea = $('.rulesContent textarea[name="Rules"]');
    if (existingTextarea.length === 0) {
        // Создаем новый элемент textarea
        const textarea = $('<textarea>', {
            class: 'h-32 shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline',
            placeholder: '1. Не нарушать правила',
            name: 'Rules',
            required: "1"
        });

        // Добавляем textarea в контейнер с классом "rulesContent"
        $('.rulesContent').append(textarea);
    }
}

function removeRules() {
    $('.rulesContent').empty();
}

function addHistory() {
    const existingTextarea = $('.historyContent textarea[name="History"]');
    if (existingTextarea.length === 0) {
        // Создаем новый элемент textarea
        const textarea = $('<textarea>', {
            class: 'h-32 shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline',
            placeholder: '1. 2023 создана платформа',
            name: 'History',
            required: "1"
        });

        // Добавляем textarea в контейнер с классом "historyContent"
        $('.historyContent').append(textarea);
    }
}

function removeHistory() {
    $('.historyContent').empty();
}

function addReview() {
    const html = `
      <div class="reviewItem flex flex-wrap -mx-3 mb-6">
        <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
          <div class="mb-1">
            <label class="block text-gray-700 text-sm font-bold mb-2">
              Имя
            </label>
            <input name="Reviews[${reviewIndex}].Name"
              class="shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
              type="text" placeholder="Иванов Василий" required/>
          </div>
          <div class="mb-1">
            <label class="block text-gray-700 text-sm font-bold mb-2">
              Ссылка на аватар
            </label>
            <input name="Reviews[${reviewIndex}].Url"
              class="shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
              type="text" placeholder="https://cdn-icons-png.flaticon.com/512/3177/3177440.png" value="https://cdn-icons-png.flaticon.com/512/3177/3177440.png" required/>
          </div>
        </div>
        <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
          <label class="block text-gray-700 text-sm font-bold mb-2">
            Текст отзыва
          </label>
          <textarea class="h-32 shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" placeholder="Лучший конкурс!!" name="Reviews[${reviewIndex}].Description" required></textarea>
        </div>
      </div>
    `;

    $('.reviewsContent').append(html);
    reviewIndex++;
}

function removeReview() {
    const lastReviewItem = $('.reviewItem:last');
    if (lastReviewItem.length > 0) {
        lastReviewItem.remove();
        reviewIndex--;
    }
}

function addNews() {
    const html = `  
    <div class="newsItem flex flex-wrap -mx-3 mb-6">
        <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
          <div class="mb-1">
            <label class="block text-gray-700 text-sm font-bold mb-2">
              Название
            </label>
            <input name="News[${newsIndex}].Name"
              class="shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
              type="text" placeholder="Была создана новая платформа для проведения олимпиад" required="">
          </div>
          <div class="mb-1">
            <label class="block text-gray-700 text-sm font-bold mb-2">
              Фото
            </label>
            <input name="News[${newsIndex}].PhotoLink"
              class="shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
              type="text" placeholder="https://cdn-icons-png.flaticon.com/512/3177/3177440.png" required="">
          </div>
          <div class="mb-1">
            <label class="block text-gray-700 text-sm font-bold mb-2">
              Ссылка
            </label>
            <input name="News[${newsIndex}].Link"
              class="shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
              type="text" placeholder="https://bspu.ru/news/1" required="">
          </div>
        </div>
        <div class="w-full md:w-1/2 h-full px-3 mb-6 md:mb-0">
          <label class="block text-gray-700 text-sm font-bold mb-2">
            Описание
          </label>
          <textarea placeholder="На днях была создана и протестирована новая платформа для проведения олимпиад" name="News[${newsIndex}].Description" required=""
            class="h-52 shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"></textarea>
        </div>
    </div>`;

    $('.newsContent').append(html);
    newsIndex++;
}

function removeNews() {
    const lastNewsItem = $('.newsItem:last');
    if (lastNewsItem.length > 0) {
        lastNewsItem.remove();
        newsIndex--;
    }
}

function addPhoto() {
    const html = `
      <div class="photoItem flex flex-wrap px-3 -mx-3 mb-6">
        <input name="PhotoUrls[${photoIndex}].Url"
          class="shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          type="text" placeholder="https://i.imgur.com/wNZKQLY.jpeg" required/>
      </div>
    `;

    $('.photoContent').append(html);
    photoIndex++;
}

function removePhoto() {
    const lastPhotoItem = $('.photoItem:last');
    if (lastPhotoItem.length > 0) {
        lastPhotoItem.remove();
        photoIndex--;
    }
}

function addStep() {
    const html = `
      <div class="stepItem flex flex-wrap -mx-3 mb-6">
        <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
          <label class="block text-gray-700 text-sm font-bold mb-2">
            Название шага
          </label>
          <input name="Steps[${stepIndex}].Name"
            class="shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-2"
            type="text" placeholder="Выбери тему" required>
          <label class="block text-gray-700 text-sm font-bold mb-2">
            * Ссылка на видео
          </label>
          <input name="Steps[${stepIndex}].VideoUrl"
            class="shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline mb-2"
            type="text" placeholder="https://rutube.ru/play/embed/c6cc4d620b1d4338901770a44b3e82f4">
        </div>
        <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
          <label class="block text-gray-700 text-sm font-bold mb-2">
            * Описание
          </label>
          <textarea class="h-32 shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" placeholder="Соберись с мыслями" name="Steps[${stepIndex}].Description" required></textarea>
        </div>
      </div>
    `;

    $('.stepsContent').append(html);
    stepIndex++;
}

function removeStep() {
    const lastStepItem = $('.stepItem:last');
    if (lastStepItem.length > 0) {
        lastStepItem.remove();
        stepIndex--;
    }
}

function addNomination() {
    const html = `
    <div class="nominationItem flex flex-wrap -mx-3 mb-6">
        <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
            <label class="block text-gray-700 text-sm font-bold mb-2">
                Название номинации
            </label>
            <input name="Nominations[${nominationIndex}].Name" class="shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" type="text" placeholder="Лучший певец" required="">
        </div>
        <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
            <label class="block text-gray-700 text-sm font-bold mb-2">
                Описание
            </label>
            <textarea class="h-32 shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" placeholder="Спой лучше всех и победи!!" name="Nominations[${nominationIndex}].Description" required=""></textarea>
        </div>
    </div>
    `;

    $('.nominationsContent').append(html);
    nominationIndex++;
}

function removeNomination() {
    const lastStepItem = $('.nominationItem:last');
    if (lastStepItem.length > 0) {
        lastStepItem.remove();
        nominationIndex--;
    }
}

function addField() {
    const html = `
        <div class="formField flex flex-wrap -mx-3 mb-6">
            <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
              <label class="block text-gray-700 text-sm font-bold mb-2">
                Название поля
              </label>
              <input name="FormFields[${fieldIndex}].Name" class="shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" type="text" placeholder="Ваш город" required>
            </div>
            <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
              <div class="flex md:w-1/2 text-center align-center mb-2">
                <label class="block text-gray-700 text-sm font-bold">
                   Значения
                </label>
                <button fieldIndex="${fieldIndex}" predefinedIndex="0" class="addPredefined flex items-center justify-center" type="button">
                  <span class="mx-1 [&amp;>svg]:h-5 [&amp;>svg]:w-5 [&amp;>svg]:text-gray-400 dark:[&amp;>svg]:text-gray-900">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="h-5 w-5">
                      <path stroke-linecap="round" stroke-linejoin="round" d="M12 4.5v15m7.5-7.5h-15"></path>
                    </svg>
                  </span>
                </button>
                <button class="removePredefined flex items-center justify-center" type="button">
                  <span class="mx-1 [&amp;>svg]:h-5 [&amp;>svg]:w-5 [&amp;>svg]:text-gray-400 dark:[&amp;>svg]:text-gray-900">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="h-5 w-5">
                      <line fill="none" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" x1="4" x2="20" y1="12" y2="12"></line>
                    </svg>
                  </span>
                </button>
              </div>
              <div class="predefinedFields">
                
              </div>
            </div>
          </div>
    `;
    $('.formFields').append(html);
    fieldIndex++;
}

function removeField() {
    const lastItem = $('.formField:last');
    if (lastItem.length > 0) {
        lastItem.remove();
        fieldIndex--;
    }
}

$(document).ready(function () {
    $('.formFields').on('click', '.addPredefined', function () {
        let fieldIndex = +$(this).attr('fieldIndex');
        let predefIndex = +$(this).attr('predefinedIndex');
        const fields = $(this).parent().parent().children().last();
        const input = `
            <input name="FormFields[${fieldIndex}].Predefined[${predefIndex}].Value" class="mb-2 shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" type="text" placeholder="Уфа" required>
        `;
        fields.append(input);
        predefIndex++;
        $(this).attr('predefinedIndex', predefIndex);
    });
    $('.formFields').on('click', '.removePredefined', function () {
        const mainBtn = $(this).prev('button');
        let predefIndex = +$(mainBtn).attr('predefinedIndex');
        const field = $(mainBtn).parent().parent().children().last().children().last();
        if (field.length > 0) {
            field.remove();
            predefIndex--;
            $(mainBtn).attr('predefinedIndex', predefIndex);
        }
    });
});

function addHelp() {
    const html = `
      <div class="helpItem flex flex-wrap -mx-3 mb-6">
        <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
          <label class="block text-gray-700 text-sm font-bold mb-2">
            Название
          </label>
          <input name="Helps[${helpIndex}].Name"
            class="shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            type="text" placeholder="Критерии оценки" required>
        </div>
        <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
          <label class="block text-gray-700 text-sm font-bold mb-2">
            Текст
          </label>
          <textarea class="h-32 shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" placeholder="1. Четкие" name="Helps[${helpIndex}].Description" required></textarea>
        </div>
      </div>
    `;

    $('.helpsContent').append(html);
    helpIndex++;
}

function removeHelp() {
    const lastItem = $('.helpItem:last');
    if (lastItem.length > 0) {
        lastItem.remove();
        helpIndex--;
    }
}

function addPartner() {
    const html = `
    <div class="flex flex-wrap -mx-3 mb-6 partnerItem">
        <div class="w-full md:w-1/3 px-3 mb-6 md:mb-0">
            <label class="block text-gray-700 text-sm font-bold mb-2">
                Организация
            </label>
            <input name="Partners[${partnerIndex}].Name"
                class="shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                type="text" placeholder="Газпром" required/>
            </div>
            <div class="w-full md:w-1/3 px-3 mb-6 md:mb-0">
            <label class="block text-gray-700 text-sm font-bold mb-2">
                Сайт
            </label>
            <input name="Partners[${partnerIndex}].Url"
                class="shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                type="text" placeholder="https://www.gazprom.ru/" required/>
            </div>
            <div class="w-full md:w-1/3 px-3 mb-6 md:mb-0">
            <label class="block text-gray-700 text-sm font-bold mb-2">
                Лого
            </label>
            <input name="Partners[${partnerIndex}].PhotoUrl"
                class="shadow-sm appearance-none border rounded w-full py-3 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                type="text" placeholder="https://cdn-icons-png.flaticon.com/512/3177/3177440.png" required/>
        </div>
    </div>
    `;
    $('.partnersContent').append(html);
    partnerIndex++;
}

function removePartner() {
    const lastItem = $('.partnerItem:last');
    if (lastItem.length > 0) {
        lastItem.remove();
        partnerIndex--;
    }
}

