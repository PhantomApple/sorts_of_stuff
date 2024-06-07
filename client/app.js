const displayModal = (data) => {
    const modal = document.getElementById("myModal");
    const modalContent = modal.querySelector(".form-group");
    document.getElementById("getBtn").addEventListener("click", qwerty);
    document.querySelector(".close").addEventListener("click", closeModal);
    
    modalContent.innerHTML = ""; 
    data.forEach(item => {
        const dataElement = document.createElement("div");
        dataElement.innerHTML = `
            <p>Date: ${item.date}</p>
            <p>Temperature: ${item.temperatureC}°C (${item.temperatureF}°F)</p>
            <p>Summary: ${item.summary}</p>
            <hr>
        `;

        modalContent.appendChild(dataElement);
    });

    modal.style.display = "block"; // Показываем модальное окно
};

const closeModal = () => {
    const modal = document.getElementById("myModal");
    modal.style.display = "none";
};

// Функция для загрузки данных по клику на кнопку
const qwerty = async function () {
    try {
        const response = await fetch("https://localhost:7232/WeatherForecast");
        const weatherData = await response.json();
        
        displayModal(weatherData);
    } catch (error) {
        console.error("Error fetching weather data:", error);
    }
};









// const displayModal = (data) => {
//     const modal = document.getElementById("myModal");
//     const modalContent = modal.querySelector(".form-group");
//     const closeBtn = modal.querySelector(".close");
//     // Очищаем содержимое модального окна перед добавлением новых данных
//     modalContent.innerHTML = "";
//     // Создаем элемент для вывода данных
//     const dataElement = document.createElement("p");
//     dataElement.textContent = JSON.stringify(data);
//     // Добавляем элемент с данными в модальное окно
//     modalContent.appendChild(dataElement);
//     // Показываем модальное окно
//     modal.style.display = "block";
//     // Добавляем обработчик события на кнопку закрытия модального окна
//     closeBtn.addEventListener("click", function () {
//         // Закрываем модальное окно при клике на кнопку закрытия
//         modal.style.display = "none";
//         // Очищаем форму в модальном окне
//         clearForm(); // описать функцию clearForm(), если требуется
//     });
// };
// const closeModal = () => {
//     const modal = document.getElementById("myModal");
//     modal.style.display = "none";
// };
// const qwerty = async function logMovies() {
//     const response = await fetch("https://localhost:7232/WeatherForecast", {
//         // mode: 'cors',
//         // headers: {
//         //     'Access-Control-Allow-Origin': '*',
//         // }
//     });
//     const movies = await response.json();
  
//     // Выводим данные в модальное окно
//     displayModal(movies);
// };