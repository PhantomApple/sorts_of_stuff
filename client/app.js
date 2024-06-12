const displayModal = (data) => {
    const modal = document.getElementById("myModal");
    const modalContent = modal.querySelector(".form-group");
    document.getElementById("getBtn").addEventListener("click", qwerty);
    document.querySelector(".close").addEventListener("click", closeModal);
    
    modalContent.innerHTML = ""; 
    data.forEach(item => {
        const dataElement = document.createElement("div");
        dataElement.innerHTML = `
        <p>id_raspes: ${item.id_raspes}</p>
        <p>id_discipline: ${item.id_discipline}</p>
        <p>id_teach: ${item.id_teach}</p>
        <p>id_group: ${item.id_group}</p>
        <p>id_office: ${item.id_office}</p>
        <p>id_user: ${item.id_user}</p>
        <p>DayNedel: ${item.dayNedel}</p>
        <p>hours_passed: ${item.hours_passed}</p>

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
        const response = await fetch("https://localhost:7232/WeatherForecast/GetPersons");
        const weatherData = await response.json();
        
        displayModal(weatherData);
    } catch (error) {
        console.error("Error fetching data:", error);
    }
};
