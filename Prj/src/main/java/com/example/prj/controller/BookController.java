package com.example.prj.controller;

import com.example.prj.entity.Book;
import com.example.prj.service.BookService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/books")
public class BookController {

    private final BookService bookService;

    public BookController(BookService bookService) {
        this.bookService = bookService;
    }

    @GetMapping
    public List<Book> getAllBooks() {
        return bookService.findAll();
    }

    @GetMapping("/{id}")
    public Book getBookById(@PathVariable Long id) {
        return bookService.findById(id);
    }

    @PostMapping
    public Book addBook(@RequestBody Book book) {
        return bookService.save(book);
    }

    @PutMapping("/{id}")
    public Book updateBook(@PathVariable Long id, @RequestBody Book updatedBook) {
        Book book = bookService.findById(id);
        book.setName(updatedBook.getName());
        book.setAuthor(updatedBook.getAuthor());
        book.setReleaseDate(updatedBook.getReleaseDate());
        book.setDescription(updatedBook.getDescription());
        book.setPenalty(updatedBook.getPenalty());
        book.setCount(updatedBook.getCount());
        book.setGenreId(updatedBook.getGenreId());
        return bookService.save(book);
    }

    @DeleteMapping("/{id}")
    public void deleteBook(@PathVariable Long id) {
        bookService.deleteById(id);
    }
}

