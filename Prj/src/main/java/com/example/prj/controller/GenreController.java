package com.example.prj.controller;

import com.example.prj.entity.Genre;
import com.example.prj.service.GenreService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController // Аннотация указывает, что этот класс является REST-контроллером и будет возвращать JSON-ответы.
@RequestMapping("/api/genres") // Базовый путь для всех HTTP-запросов к этому контроллеру.
public class GenreController {

    // Поле для связи с сервисным слоем, обеспечивающее обработку логики жанров.
    private final GenreService genreService;

    // Конструктор с внедрением зависимости GenreService.
    public GenreController(GenreService genreService) {
        this.genreService = genreService;
    }

    /**
     * Возвращает список всех жанров.
     * @return Список объектов Genre.
     */
    @GetMapping // Обрабатывает HTTP-запросы GET по пути "/api/genres".
    public List<Genre> getAllGenres() {
        return genreService.getAllGenres(); // Вызывает метод сервиса для получения всех жанров.
    }

    /**
     * Добавляет новый жанр.
     * @param genre Объект Genre, передаваемый в теле запроса.
     * @return Сохраненный объект Genre.
     */
    @PostMapping // Обрабатывает HTTP-запросы POST по пути "/api/genres".
    public Genre addGenre(@RequestBody Genre genre) { // @RequestBody указывает, что данные жанра приходят в теле запроса.
        return genreService.saveGenre(genre); // Вызывает метод сервиса для сохранения жанра.
    }

    /**
     * Обновляет жанр по его идентификатору.
     * @param id Идентификатор жанра.
     * @param genre Объект Genre с обновленными данными.
     * @return Обновленный объект Genre.
     */
    @PutMapping("/{id}") // Обрабатывает HTTP-запросы PUT по пути "/api/genres/{id}".
    public Genre updateGenre(@PathVariable Long id, @RequestBody Genre genre) { // @PathVariable связывает параметр id с частью URL.
        return genreService.updateGenre(id, genre); // Вызывает метод сервиса для обновления жанра.
    }

    /**
     * Удаляет жанр по его идентификатору.
     * @param id Идентификатор жанра.
     */
    @DeleteMapping("/{id}") // Обрабатывает HTTP-запросы DELETE по пути "/api/genres/{id}".
    public void deleteAuthor(@PathVariable Long id) {
        genreService.deleteById(id); // Вызывает метод сервиса для удаления жанра.
    }
}
