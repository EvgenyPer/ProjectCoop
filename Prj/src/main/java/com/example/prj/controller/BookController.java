package com.example.prj.controller;

import com.example.prj.entity.Book;
import com.example.prj.service.BookService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

// Аннотация @RestController указывает, что данный класс является контроллером REST API.
@RestController
// @RequestMapping задает базовый URL для всех методов в контроллере.
@RequestMapping("/books")
public class BookController {

    // Поле для работы с сервисом книг.
    private final BookService bookService;

    // Конструктор для внедрения зависимости BookService.
    public BookController(BookService bookService) {
        this.bookService = bookService;
    }

    // Метод для получения списка всех книг.
    @GetMapping
    public List<Book> getAllBooks() {
        return bookService.findAll();
    }

    // Метод для получения книги по идентификатору.
    @GetMapping("/{id}")
    public Book getBookById(@PathVariable Long id) {
        return bookService.findById(id);
    }

    // Метод для добавления новой книги.
    @PostMapping
    public Book addBook(@RequestBody Book book) {
        return bookService.save(book);
    }

    // Метод для обновления существующей книги.
    @PutMapping("/{id}")
    public Book updateBook(@PathVariable Long id, @RequestBody Book updatedBook) {
        // Находим книгу по идентификатору.
        Book book = bookService.findById(id);

        // Обновляем поля книги.
        book.setName(updatedBook.getName());
        book.setAuthor(updatedBook.getAuthor());
        book.setReleaseDate(updatedBook.getReleaseDate());
        book.setDescription(updatedBook.getDescription());
        book.setPenalty(updatedBook.getPenalty());
        book.setCount(updatedBook.getCount());
        book.setGenreId(updatedBook.getGenreId());

        // Сохраняем обновленную книгу в базе данных.
        return bookService.save(book);
    }

    // Метод для удаления книги по идентификатору.
    @DeleteMapping("/{id}")
    public void deleteBook(@PathVariable Long id) {
        bookService.deleteById(id);
    }
}
