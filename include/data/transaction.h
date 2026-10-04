#ifndef TRANSACTION_H
#define TRANSACTION_H

#include <string>
#include <cstdint>
#include <chrono>
#include <optional>

class transaction
{
public:
    std::uint64_t id() const;
    void id(std::uint64_t transaction_id);

    std::uint64_t account_id() const;
    void account_id(std::uint64_t acc_id);

    const std::string& label() const;
    void label(std::string transaction_label);

    const std::optional<std::string>& note() const;
    void note(std::optional<std::string> transaction_note);

    const std::optional<std::string>& payment_method() const;
    void payment_method(std::optional<std::string> method);

    const std::optional<std::string>& check_number() const;
    void check_number(std::optional<std::string> ck_number);

    std::chrono::system_clock::time_point created_at() const;
    void created_at(std::chrono::system_clock::time_point creation_time);

    std::chrono::year_month_day accounting_date() const;
    void accounting_date(std::chrono::year_month_day date);

    std::optional<std::chrono::year_month_day> value_date() const;
    void value_date(std::optional<std::chrono::year_month_day> date);

    bool is_reconciled() const;
    void is_reconciled(bool reconciled);

    std::int64_t amount() const;
    void amount(std::int64_t cents);

private:
    std::uint64_t id_{0};
    std::uint64_t account_id_{0};
    std::string label_;
    std::optional<std::string> note_;
    std::optional<std::string> payment_method_;
    std::optional<std::string> check_number_;
    std::chrono::system_clock::time_point created_at_{};
    std::chrono::year_month_day accounting_date_{};
    std::optional<std::chrono::year_month_day> value_date_;
    bool is_reconciled_{false};
    std::int64_t amount_{0};
};

#endif  // TRANSACTION_H