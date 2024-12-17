package com.example.prj.controller;

// Импорт сущности Author
import com.example.prj.entity.Author;
// Импорт сервисного слоя AuthorService
import com.example.prj.service.AuthorService;
// Аннотация @RestController указывает, что данный класс является REST-контроллером
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
// Указывает базовый URL для всех запросов в этом контроллере
@RequestMapping("/api/authors")
public class AuthorController {

    // Поле для взаимодействия с AuthorService
    private final AuthorService authorService;

    // Конструктор для внедрения зависимости AuthorService
    public AuthorController(AuthorService authorService) {
        this.authorService = authorService;
    }

    // Метод для получения списка всех авторов (HTTP GET /api/authors)
    @GetMapping
    public List<Author> getAllAuthors() {
        return authorService.getAllAuthors();
    }

    // Метод для добавления нового автора (HTTP POST /api/authors)
    @PostMapping
    public Author addAuthor(@RequestBody Author author) {
        return authorService.saveAuthor(author);
    }

    // Метод для обновления данных автора (HTTP PUT /api/authors/{id})
    @PutMapping("/{id}")
    public Author updateAuthor(@PathVariable Long id, @RequestBody Author author) {
        return authorService.updateAuthor(id, author);
    }

    // Метод для удаления автора по ID (HTTP DELETE /api/authors/{id})
    @DeleteMapping("/{id}")
    public void deleteAuthor(@PathVariable Long id) {
        authorService.deleteById(id);
    }
}
