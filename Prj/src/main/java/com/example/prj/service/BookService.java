package com.example.prj.service;

import com.example.prj.entity.Book;
import com.example.prj.repository.BookRepository;
import org.springframework.stereotype.Service;

import java.util.List;

// Аннотация @Service указывает, что данный класс является сервисным компонентом Spring.
@Service
public class BookService {

    // Поле для работы с репозиторием книг.
    private final BookRepository bookRepository;

    // Конструктор для внедрения зависимости BookRepository.
    public BookService(BookRepository bookRepository) {
        this.bookRepository = bookRepository;
    }

    // Метод для получения всех книг из базы данных.
    public List<Book> findAll() {
        return bookRepository.findAll();
    }

    // Метод для поиска книги по идентификатору.
    // Если книга не найдена, выбрасывается исключение.
    public Book findById(Long id) {
        return bookRepository.findById(id).orElseThrow(() -> new RuntimeException("Book not found"));
    }

    // Метод для сохранения новой книги или обновления существующей.
    public Book save(Book book) {
        return bookRepository.save(book);
    }

    // Метод для удаления книги по идентификатору.
    public void deleteById(Long id) {
        bookRepository.deleteById(id);
    }
}
